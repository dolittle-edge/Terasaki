// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace RaaLabs.Edge.Connectors.Terasaki
{
    /// <summary>
    /// 
    /// </summary>
    static class TerasakiStreamReader
    {
        /// <summary>
        /// Read from the stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async IAsyncEnumerable<string> ReadLineAsync(Stream stream)
        {
            List<byte> line = new List<byte>();
            var byteStreamEnumerator = ReadAsync(stream).GetAsyncEnumerator();
            while (true)
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
            byte[] buffer = new byte[1024];

            while (true)
            {
                var size = await stream.ReadAsync(buffer.AsMemory(0, 1024));

                foreach (var i in Enumerable.Range(0, size))
                {
                    yield return buffer[i];
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