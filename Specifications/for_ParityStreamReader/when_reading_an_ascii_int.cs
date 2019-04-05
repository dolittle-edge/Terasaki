/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using Machine.Specifications;

namespace Dolittle.Edge.Terasaki.for_ParityStreamReader
{
    public class when_reading_an_ascii_int
    {
        static byte[] bytes = 
        {
            0x34, 0x32
        };

        static ParityStreamReader reader;

        static int result;

        Establish context = () => reader = new ParityStreamReader(new MemoryStream(bytes));

        Because of = () => result = reader.ReadAsciiInt(2);

        It should_return_the_expected_number = () => result.ShouldEqual(42);
    }
}