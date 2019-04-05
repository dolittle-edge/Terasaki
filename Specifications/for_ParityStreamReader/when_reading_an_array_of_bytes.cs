/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Machine.Specifications;

namespace Dolittle.Edge.Terasaki.for_ParityStreamReader
{
    public class when_reading_an_array_of_bytes
    {
        static byte[] bytes = 
        {
            0x42, 0x43, 0x84
        };

        static ParityStreamReader reader;

        static byte[] result;

        Establish context = () => reader = new ParityStreamReader(new MemoryStream(bytes));

        Because of = () => result = reader.ReadBytes(3);

        It should_return_the_bytes_in_the_stream = () => result.ShouldEqual(bytes);
        It should_hold_the_correct_parity = () => ((int)reader.Parity).ShouldEqual(bytes[0]^bytes[1]^bytes[2]);
    }
}