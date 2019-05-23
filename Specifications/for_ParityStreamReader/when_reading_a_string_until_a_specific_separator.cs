/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Machine.Specifications;

namespace Dolittle.TimeSeries.Terasaki.for_ParityStreamReader
{
    public class when_reading_a_string_until_a_specific_separator
    {
        static byte[] bytes = 
        {
            0x41, 0x42, 0x43, 0x2a
        };

        static ParityStreamReader reader;

        static string result;

        Establish context = () => reader = new ParityStreamReader(new MemoryStream(bytes));

        Because of = () => result = reader.ReadUntil((char)0x2a);

        It should_return_the_expected_number = () => result.ShouldEqual("ABC");
    }    
}