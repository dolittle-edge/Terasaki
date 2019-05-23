/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Machine.Specifications;

namespace Dolittle.TimeSeries.Terasaki.for_ParityStreamReader
{
    public class when_reading_a_byte_after_skipping
    {
        static byte[] bytes = 
        {
            0x41, 0x42, 0x2, 0x43
        };

        static ParityStreamReader reader;

        static byte result;

        Establish context = () => reader = new ParityStreamReader(new MemoryStream(bytes));

        Because of = () => 
        {
            reader.SkipTill(0x2);
            result = reader.ReadByte();
        };


        It should_return_the_expected_byte = () => ((int)result).ShouldEqual(0x43);
    }    
}