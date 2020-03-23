/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace RaaLabs.TimeSeries.Terasaki.for_Parser.given
{
    public abstract class a_parser_that_streams_bytes
    {
        protected static SentenceParser parser;

        static MemoryStream stream;

        protected static byte[] bytes;
        protected static Dictionary<Tag, TagWithData> dataPoints;

        protected static Exception exception;

        Establish context = () =>
        {
            parser = new SentenceParser();
            dataPoints = new Dictionary<Tag, TagWithData>();
        };

        Because of = () => 
        {
            stream = new MemoryStream(bytes);
            try
            {
                foreach (var line in TerasakiStreamReader.ReadLine(stream))
                {
                    var parsed = parser.Parse(line).ToDictionary(d => d.Tag, d => d);
                    dataPoints = dataPoints.Union(parsed).ToDictionary(d => d.Key, d => d.Value);
                }
            }
            catch (Exception e)
            {
                exception = e;
            }
        };
    }
}