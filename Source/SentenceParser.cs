/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Dolittle.Collections;
using Dolittle.Types;
using Dolittle.Logging;


namespace RaaLabs.TimeSeries.Terasaki
{
    /// <summary>
    /// Represents an implementation of <see cref="ISentenceParser"/>
    /// </summary>
    public class SentenceParser : ISentenceParser
    {
        readonly ILogger _logger;

        /// <summary>
        ///  Initializes a new instance of <see cref="SentenceParser"/>
        /// </summary>
        /// <param name="logger"></param>
        public SentenceParser(
            ILogger logger
            )
        {
            _logger = logger;

        }



        /// <inheritdoc/>
        public bool CanParse(string sentence)
        {
            if (!IsValidSentence(sentence)) return false;
            //sentence = sentence.Substring(1);
            return true;
        }

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string sentence)
        {
            sentence = sentence.TrimEnd();
            var data = new List<TagWithData>();
            var originalSentence = sentence;
            var formatIdentifier = sentence.Substring(1, 6);
            var frame = formatIdentifier.Substring(0, 3);
            var numberofrecords = formatIdentifier.Substring(4, 2);

            if (sentence[sentence.Length - 3] == '*')
            {
                var checksum = Byte.Parse(sentence.Substring(sentence.Length - 2), NumberStyles.HexNumber);
                //sentence = sentence.Substring(1,sentence.Length-5);
                sentence = sentence.Substring(sentence.IndexOf("$") + 1, sentence.IndexOf("*") - 1);
                byte calculatedChecksum = 0;
                for (var i = 0; i < sentence.Length; i++) calculatedChecksum ^= (byte)sentence[i];
                ThrowIfSentenceChecksumIsInvalid(sentence, checksum, calculatedChecksum);

            }
            else sentence = sentence.Substring(1);

            var values = sentence.Substring(7).Split(',');
            for (var record = 0; record < Int32.Parse(numberofrecords); record++)
            {
                var payload = values[record];
                var tag = $"{frame}:{record + 1}";
                try
                {
                    if (payload.Any(char.IsDigit))
                    {
                        data.Add(new TagWithData(tag, float.Parse(payload)));
                    }
                }
                catch (FormatException ex)
                {
                    _logger.Error(ex, $"Trouble parsing  {tag} : {payload}");
                }
            }

            return data;

        }

        bool IsValidSentence(string sentence)
        {
            if (!sentence.StartsWith('$')) return false;
            if (sentence.Length < 7) return false;
            if (!sentence.Substring(1, 6).All(char.IsDigit)) return false;
            return true;
        }

        void ThrowIfSentenceChecksumIsInvalid(string sentence, byte actualChecksum, byte expectedChecksum)
        {
            if (expectedChecksum != actualChecksum) throw new InvalidSentenceChecksum(actualChecksum, expectedChecksum, sentence);
        }
    }
}