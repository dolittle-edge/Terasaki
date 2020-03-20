using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace RaaLabs.TimeSeries.Terasaki
{
    /// <inheritdoc/>
    public class SentenceParser : ISentenceParser
    {
        readonly Regex _sentencePattern;
        readonly Regex _valuePattern;

        /// <summary>
        ///  Initializes a new instance of <see cref="SentenceParser"/>
        /// </summary>
        public SentenceParser()
        {
            _sentencePattern = new Regex(@"^(\d{6},?)(.*),\s*\*([0-9A-F]{2})\r?$");
            _valuePattern = new Regex(@"([a-zA-Z]*)\s*(.*)");
        }

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string sentence)
        {
            // Calculate the checksum by folding over the sentence up to the asterisk (*) character, using XOR
            byte calculatedChecksum = sentence.TakeWhile(c => c != '*').Aggregate((byte)0x00, (sum, next) => (byte)(sum ^ next));

            // Extract the different parts of the sentence using regex
            var (formatIdentifier, payload, checksumStr, _) = _sentencePattern.Match(sentence).Groups.Skip(1).Select(g => g.Value).ToList();
            var checksum = Byte.Parse(checksumStr, System.Globalization.NumberStyles.HexNumber);

            if (checksum != calculatedChecksum) throw new InvalidSentenceChecksum(checksum, calculatedChecksum, sentence);

            List<string> values = payload.Split(",").ToList();

            var frame = formatIdentifier.Substring(0, 3);
            var numberOfRecords = Int32.Parse(formatIdentifier.Substring(3, 3));

            if (numberOfRecords != values.Count) throw new Exception("Invalid number of channels");

            return values.SelectMany((value, index) => ParseValue(frame, index + 1, value));
        }

        private IEnumerable<TagWithData> ParseValue(string frame, int record, string value)
        {
            var parsedFrame = int.Parse(frame);
            var tag = $"{parsedFrame}:{record}";
            var (state, valueStr, _) = _valuePattern.Match(value).Groups.Skip(1).Select(g => g.Value).ToList();
            var successful = float.TryParse(valueStr, out float parsedValue);

            return successful ? new List<TagWithData> { new TagWithData(tag, parsedValue) } : new List<TagWithData>();
        }
    }
}
