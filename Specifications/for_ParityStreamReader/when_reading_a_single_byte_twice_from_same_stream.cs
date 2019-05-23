/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Machine.Specifications;

namespace Dolittle.TimeSeries.Terasaki.for_ParityStreamReader
{


    public class when_reading_a_single_byte_twice_from_same_stream
    {
        static byte[] bytes = 
        {
            0x42, 0x43
        };

        static ParityStreamReader reader;

        static byte first_byte;
        static byte second_byte;

        Establish context = () => reader = new ParityStreamReader(new MemoryStream(bytes));

        Because of = () => 
        {
            first_byte = reader.ReadByte();
            second_byte = reader.ReadByte();
        };
            

        It should_read_the_expected_first_byte = () => first_byte.ShouldEqual(bytes[0]);
        It should_read_the_expected_second_byte = () => second_byte.ShouldEqual(bytes[1]);
    }
}