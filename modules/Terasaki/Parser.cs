/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
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
                var reader = new ParityStreamReader(stream);

                // Keep track of the blocks
                var lastSeenBlock = -1;
                var channelIdOffset = 0;

                for(;;)
                {
                    reader.SkipTillStartOfBlock();

                    var blockNumber = reader.ReadAsciiInt(3);
                    var numberOfChannels = reader.ReadAsciiInt(3);

                    var outOfOrderBlock = false;

                    if (blockNumber == 0)
                    {
                        lastSeenBlock = 0;
                        channelIdOffset = 0;
                    }
                    else if (blockNumber == lastSeenBlock+1)
                    {
                        lastSeenBlock = blockNumber;
                    }
                    else
                    {
                        outOfOrderBlock = true;
                        _logger.Warning($"An out of order block '{blockNumber}' was detected, data will have to be thrown away.");
                    }

                    _logger.Information($"Handling block '{blockNumber}' with '{numberOfChannels}' channels");

                    var channels = new Channel[numberOfChannels];
                    for (var i = 0; i < numberOfChannels; ++i)
                    {
                        var channelData = reader.ReadUntil(',');

                        var channelState = ' ';
                        var channelValue = 0.0;

                        if (channelData[0] != ' ' && (channelData[0] < '0' || channelData[0] > '9'))
                        {
                            // There is something other than a number in the first character
                            channelState = channelData[0];
                            double.TryParse(channelData.AsSpan().Slice(1), out channelValue);
                        }
                        else
                        {
                            // Just a number
                            double.TryParse(channelData, out channelValue);
                        }

                        channels[i] = new Channel
                        {
                            Id = i+channelIdOffset,
                            Value = new ChannelValue {
                                State = channelState,
                                Value = channelValue,
                                ParityError = false,
                            },
                        };
                    }

                    var calculatedParity = reader.Parity;

                    if (reader.ReadByte() != '*') throw new InvalidDataException("Expected '*' after all channel data");

                    var transmittedParity = reader.ReadAsciiHexByte();

                    if (calculatedParity != transmittedParity)
                    {
                        _logger.Warning($"A parity error was detected while reading block '{blockNumber}'.");
                    }

                    reader.ReadByte(); // There should be an end-of-text here, but there's no need to check

                    // Transmit the data if the block was in order
                    if (!outOfOrderBlock)
                    {
                        for (var i = 0; i < numberOfChannels; ++i)
                        {
                            channels[i].Value.ParityError = calculatedParity != transmittedParity;
                            callback(channels[i]);
                        }
                    }

                    // Keep track of channels seen
                    channelIdOffset += numberOfChannels;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error while parsing");
            }
        }
   }   
}