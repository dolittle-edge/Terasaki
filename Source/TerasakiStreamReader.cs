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
    public class TerasakiStreamReader
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
                byte[] line;
                try
                {
                    var _ = dataStream.TakeWhile(b => !IsStartByte(b)).ToArray();
                    line = dataStream.TakeWhile(b => !IsStopByte(b)).ToArray();
                }
                catch (EndOfStreamException)
                {
                    yield break;
                }
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

        private static bool IsStartByte(byte b)
        {
            return b == 0x2 || b == '$';
        }

        private static bool IsStopByte(byte b)
        {
            return b == 0x3 || b == '\n';
        }
    }
}
