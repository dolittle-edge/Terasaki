using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Serilog;

namespace RaaLabs.Edge.Connectors.Terasaki
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async IAsyncEnumerable<string> ReadLineAsync(Stream stream)
        {
            List<byte> line = new List<byte>();
            var byteStreamEnumerator = ReadAsync(stream).GetAsyncEnumerator();
            while(true)
            {
                while (await byteStreamEnumerator.MoveNextAsync())
                    if (IsStartByte(byteStreamEnumerator.Current)) break;

                while (await byteStreamEnumerator.MoveNextAsync())
                {
                    if (IsStopByte(byteStreamEnumerator.Current)) break;
                    line.Add(byteStreamEnumerator.Current);
                }

                var lineString = Encoding.ASCII.GetString(line.ToArray());
                yield return lineString;

                line = new List<byte>();
            }
        }

        private static async IAsyncEnumerable<byte> ReadAsync(Stream stream)
        {
            Memory<byte> buffer = new Memory<byte>(new byte[1024]);
            byte[] bufferr = new byte[1024];

            while (true)
            {
                var sizz = await stream.ReadAsync(bufferr, 0, 1024);
                //var size = await stream.ReadAsync(buffer);

                foreach (var i in Enumerable.Range(0, sizz))
                {
                    yield return bufferr[i];
                }
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
