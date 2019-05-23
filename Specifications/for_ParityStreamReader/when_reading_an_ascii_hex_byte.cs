/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Machine.Specifications;

namespace Dolittle.TimeSeries.Terasaki.for_ParityStreamReader
{
    public class when_reading_an_ascii_hex_byte
    {
        static byte[] bytes = 
        {
            0x34, 0x32
        };

        static ParityStreamReader reader;

        static byte result;

        Establish context = () => reader = new ParityStreamReader(new MemoryStream(bytes));

        Because of = () => result = reader.ReadAsciiHexByte();

        It should_return_the_expected_number = () => ((int)result).ShouldEqual(0x42);
    }    
}