/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Machine.Specifications;

namespace Dolittle.TimeSeries.Terasaki.for_ParityStreamReader
{
    public class when_reading_a_single_byte_from_a_stream_that_is_at_the_end
    {
        static ParityStreamReader reader;
        static Exception exception;

        Establish context = () => reader = new ParityStreamReader(new MemoryStream());

        Because of = () => exception = Catch.Exception(() => reader.ReadByte());

        It should_throw_end_of_stream_exception = () => exception.ShouldBeOfExactType<EndOfStreamException>();
    }
}