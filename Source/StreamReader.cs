using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace RaaLabs.TimeSeries.Terasaki
{
    /// <summary>
    /// 
    /// </summary>
    public class StreamReader
    {
        /// <summary>
        /// Read from the stream, one line at a time.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static IEnumerable<string> ReadLine(Stream stream)
        {
            var dataStream = Read(stream);

            while (true)
            {
                dataStream.SkipWhile(b => (b != 0x2) && (b != '$')).Skip(1);
                var line = dataStream.TakeWhile(b => (b != 0x3) && (b != '\n')).Skip(1).ToArray();
                yield return Encoding.ASCII.GetString(line);
            }
        }

        private static IEnumerable<byte> Read(Stream stream)
        {
            while(true)
            {
                var data = stream.ReadByte();
                if (data < 0) throw new EndOfStreamException();
                yield return (byte)data;
            }
        }
    }
}
