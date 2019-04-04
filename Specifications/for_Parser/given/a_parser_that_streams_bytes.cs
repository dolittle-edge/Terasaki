/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.IO;
using Dolittle.Logging;
using Machine.Specifications;
using Moq;

namespace Dolittle.Edge.Terasaki.for_Parser.given
{
    public abstract class a_parser_that_streams_bytes
    {
        protected static Parser parser;

        static MemoryStream stream;
        protected static List<Channel> channels;

        protected static byte[] bytes;
        
        Establish context = () => parser = new Parser(Mock.Of<ILogger>());

        Because of = () => 
        {
            channels = new List<Channel>();
            stream = new MemoryStream(bytes);
            parser.BeginParse(stream, channels.Add);
        };


    }
}