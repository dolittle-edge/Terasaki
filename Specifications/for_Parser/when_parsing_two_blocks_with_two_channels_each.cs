/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace RaaLabs.TimeSeries.Terasaki.for_Parser
{
    public class when_parsing_two_blocks_with_two_channels_each : given.a_parser_that_streams_bytes
    {
        Establish context = () => bytes = new byte[]
            {
                0x02,                                                   // Begin block
                0x30, 0x30, 0x30,                                       // 000 block
                0x30, 0x30, 0x32,                                       // 2 channels
                0x20, 0x20, 0x2D, 0x30, 0x2E, 0x31, 0x2C,               // Whitespace state, value -0.1
                0x52, 0x20, 0x34, 0x32, 0x2E, 0x32, 0x37, 0x30, 0x2C,   // R state, value 42.270
                0x2A, 0x36, 0x46,                                       // Checksum
                0x03,                                                   // End block

                0x02,                                                       // Begin block
                0x30, 0x30, 0x31,                                           // 001 block
                0x30, 0x30, 0x32,                                           // 2 channels
                0x42, 0x20, 0x2D, 0x34, 0x32, 0x2E, 0x32, 0x37, 0x30, 0x2C, // B state, value -42.270
                0x43, 0x20, 0x30, 0x2E, 0x31, 0x2C,                         // C state, value 0.1
                0x2A, 0x31, 0x44,                                           // Checksum
                0x03                                                        // End block

            };

        It should_add_four_channels = () => dataPoints.Count.ShouldEqual(4);

        It should_hold_expected_value_for_first_channel = () => ((float) dataPoints["000:1"].Data).ShouldEqual(-0.1f);
        It should_hold_expected_value_for_second_channel = () => ((float) dataPoints["000:2"].Data).ShouldEqual(42.270f);
        It should_hold_expected_value_for_third_channel = () => ((float) dataPoints["001:1"].Data).ShouldEqual(-42.270f);
        It should_hold_expected_value_for_fourth_channel = () => ((float) dataPoints["001:2"].Data).ShouldEqual(0.1f);
    }
}
