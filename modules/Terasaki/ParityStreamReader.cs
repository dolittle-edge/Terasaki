/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Text;

namespace Dolittle.Edge.Terasaki
{
    /// <summary>
    /// Represents a <see cref="Stream"/> wrapper for dealing with the parity and Terasaki
    /// </summary>   
    public class ParityStreamReader
    {
        readonly Stream _stream;

        /// <summary>
        /// Initializes a new instance of <see cref="ParityStreamReader"/>
        /// </summary>
        /// <param name="stream">The inner <see cref="Stream"/> being used</param>
        public ParityStreamReader(Stream stream)
        {
            _stream = stream;
        }

        /// <summary>
        /// Get the current parity
        /// </summary>
        public byte Parity { get; private set; }

        /// <summary>
        /// Skip bytes till we get the start og block byte
        /// </summary>
        public void SkipTilStartOfBlock()
        {
            while (ReadByte() != 0x02);
            Parity = 0;
        }

        /// <summary>
        /// Read a single byte
        /// </summary>
        /// <returns><see cref="byte"/> read</returns>
        public byte ReadByte()
        {
            var data = _stream.ReadByte();
            if (data < 0) throw new EndOfStreamException();
            Parity ^= (byte) data;
            return (byte) data;
        }

        /// <summary>
        /// Read an array of bytes
        /// </summary>
        /// <param name="size">Number of bytes to read</param>
        /// <returns>Array of <see cref="byte">bytes</see> read</returns>
        public byte[] ReadBytes(int size)
        {
            var data = new byte[size];
            for (var i = 0; i < size; ++i)
            {
                data[i] = ReadByte();
            }
            return data;
        }

        /// <summary>
        /// Read an Ascii integer
        /// </summary>
        /// <param name="size">Number of bytes representing the integer</param>
        /// <returns>Integer from the bytes</returns>
        public int ReadAsciiInt(int size)
        {
            var data = ReadBytes(size);
            return int.Parse(Encoding.ASCII.GetString(data));
        }

        /// <summary>
        /// Read byte represented as Ascii hex
        /// </summary>
        /// <returns>The <see cref="byte"/></returns>
        public byte ReadAsciiHexByte()
        {
            var data = new [] { ReadByte(), ReadByte() };
            return Convert.ToByte(Encoding.ASCII.GetString(data), 16);
        }

        /// <summary>
        /// Read a string until a specific separator occurs in the stream
        /// </summary>
        /// <param name="separator">Separator as <see cref="char"/></param>
        /// <returns>The <see cref="string"/> read</returns>
        public string ReadUntil(char separator)
        {
            var read = 0;
            var data = new byte[10];
            var current = ReadByte();
            while (current != separator)
            {
                if (read == data.Length)
                {
                    Array.Resize(ref data, data.Length * 2);
                }
                data[read] = current;
                read++;
                current = ReadByte();
            }
            return Encoding.ASCII.GetString(data, 0, read);
        }
    }

}