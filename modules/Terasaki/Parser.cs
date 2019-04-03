/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Dolittle.Logging;

namespace Dolittle.Edge.Terasaki
{
    /// <summary>
    /// Represents an implementation of <see cref="IParser"/>
    /// </summary>
    public class Parser : IParser
    {
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="ILogger"/>
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> to use for logging</param>
        public Parser(ILogger logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public void BeginParse(Stream stream, Action<Channel> callback)
        {
            try
            {
                var channelIdOffset = 0;

                var startMarker = new byte[1];
                while (stream.Read(startMarker, 0, startMarker.Length) == startMarker.Length)
                {
                    if (startMarker[0] == 0x2)
                    {
                        var blockTypeBytes = new byte[3];
                        if (stream.Read(blockTypeBytes, 0, blockTypeBytes.Length) != blockTypeBytes.Length) return;

                        var blockType = int.Parse(Encoding.ASCII.GetString(blockTypeBytes));
                        

                        if (blockType > 0) channelIdOffset = 0;

                        var numberOfChannelsBytes = new byte[3];
                        if (stream.Read(numberOfChannelsBytes, 0, numberOfChannelsBytes.Length) != numberOfChannelsBytes.Length) return;

                        var numberOfChannels = int.Parse(Encoding.ASCII.GetString(numberOfChannelsBytes));

                        _logger.Information($"Handling blocktype '{blockType}' with '{numberOfChannels}' channels");

                        var currentValueBuffer = new byte[20]; // Magic number, averge is about 9 bytes - including state and comma
                        var currentValueBufferOffset = 0;
                        var channel = 0;

                        var singleByte = new byte[1];
                        while (stream.Read(singleByte, 0, singleByte.Length) == singleByte.Length)
                        {
                            if (singleByte[0] == 0x2a) // checksum
                            {
                                break;
                            }

                            if (singleByte[0] == 0x2c) // comma
                            {
                                var channelId = channelIdOffset + channel;
                                var state = (char) currentValueBuffer[0];
                                var valueBytes = new byte[currentValueBufferOffset - 1];

                                Buffer.BlockCopy(currentValueBuffer, 1, valueBytes, 0, valueBytes.Length);

                                var valueString = Encoding.ASCII.GetString(valueBytes);
                                double value = 0;
                                double.TryParse(valueString, NumberStyles.Any, CultureInfo.InvariantCulture, out value);

                                callback(new Channel
                                {
                                    Id = channelId,
                                        Value = new ChannelValue { Value = value, State = state }
                                });
                                currentValueBufferOffset = 0;
                                channel++;
                            }
                            else
                            {
                                currentValueBuffer[currentValueBufferOffset++] = singleByte[0];
                            }
                        }

                        channelIdOffset += numberOfChannels;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error while parsing");
            }
        }
    }
}