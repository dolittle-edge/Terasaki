/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;

namespace Dolittle.TimeSeries.Terasaki.for_Parser
{
    public class when_parsing_two_blocks_with_two_channels_each_with_different_states : given.a_parser_that_streams_bytes
    {
        Establish context = () => bytes = new byte[]
            {
                0x02,                                                   // Begin block
                0x30, 0x30, 0x30,                                       // 000 block
                0x30, 0x30, 0x32,                                       // 2 channels
                0x20, 0x20, 0x2D, 0x30, 0x2E, 0x31, 0x2C,               // Whitespace state, value -0.1
                0x52, 0x20, 0x34, 0x32, 0x2E, 0x32, 0x37, 0x30, 0x2C,   // R state, value 42.270
                0x2A, 0x31, 0x35,                                       // Checksum
                0x03,                                                   // End block

                0x02,                                                       // Begin block
                0x30, 0x30, 0x31,                                           // 001 block
                0x30, 0x30, 0x32,                                           // 2 channels
                0x42, 0x20, 0x2d, 0x34, 0x32, 0x2E, 0x32, 0x37, 0x30, 0x2C, // B state, value -42.270
                0x43, 0x20, 0x30, 0x2E, 0x31, 0x2C,                         // C state, value 0.1
                0x2A, 0x31, 0x35,                                           // Checksum
                0x03                                                        // End block

            };

        It should_add_four_channels = () => channels.Count.ShouldEqual(4);
        It should_hold_empty_state_for_first_channel = () => channels[0].Value.State.ShouldEqual(' ');
        It should_hold_R_state_for_second_channel = () => channels[1].Value.State.ShouldEqual('R');
        It should_hold_B_state_for_third_channel = () => channels[2].Value.State.ShouldEqual('B');
        It should_hold_C_state_for_fourth_channel = () => channels[3].Value.State.ShouldEqual('C');

        It should_hold_expected_value_for_first_channel = () => channels[0].Value.Value.ShouldEqual(-0.1);
        It should_hold_expected_value_for_second_channel = () => channels[1].Value.Value.ShouldEqual(42.270);
        It should_hold_expected_value_for_third_channel = () => channels[2].Value.Value.ShouldEqual(-42.270);
        It should_hold_expected_value_for_fourth_channel = () => channels[3].Value.Value.ShouldEqual(0.1);

        It should_set_channel_id_for_first_channel_to_zero = () => channels[0].Id.ShouldEqual(0);
        It should_set_channel_id_for_second_channel_to_one = () => channels[1].Id.ShouldEqual(1);
        It should_set_channel_id_for_third_channel_to_two = () => channels[2].Id.ShouldEqual(2);
        It should_set_channel_id_for_fourth_channel_to_three = () => channels[3].Id.ShouldEqual(3);
    }
}