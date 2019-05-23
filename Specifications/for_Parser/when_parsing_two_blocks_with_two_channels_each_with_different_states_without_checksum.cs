/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Machine.Specifications;

namespace Dolittle.TimeSeries.Terasaki.for_Parser
{
    public class when_parsing_two_blocks_with_two_channels_each_with_different_states_without_checksum : given.a_parser_that_streams_bytes
    {
        Establish context = () => bytes = new byte[]
            {
                0x02,                                                   // Begin block
                0x30, 0x30, 0x30,                                       // 000 block
                0x30, 0x30, 0x32,                                       // 2 channels
                0x20, 0x20, 0x2D, 0x30, 0x2E, 0x31, 0x2C,               // Whitespace state, value -0.1
                0x52, 0x20, 0x34, 0x32, 0x2E, 0x32, 0x37, 0x30, 0x2C,   // R state, value 42.270
                0x03,                                                   // End block

                0x02,                                                       // Begin block
                0x30, 0x30, 0x31,                                           // 001 block
                0x30, 0x30, 0x32,                                           // 2 channels
                0x42, 0x20, 0x2d, 0x34, 0x32, 0x2E, 0x32, 0x37, 0x30, 0x2C, // B state, value -42.270
                0x43, 0x20, 0x30, 0x2E, 0x31, 0x2C,                         // C state, value 0.1
                0x03                                                        // End block

            };

        It should_throw_invalid_data_exception = () => exception.ShouldBeOfExactType<InvalidDataException>();
    }
}