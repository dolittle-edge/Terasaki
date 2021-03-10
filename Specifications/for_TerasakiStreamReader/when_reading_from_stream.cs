/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.IO;
using System.Text;
using Machine.Specifications;
using RaaLabs.Edge.Connectors.Terasaki;
using System.Linq;

namespace RaaLabs.Edge.Connectors.Terasaki.for_TerasakiStreamReader
{
    public class when_reading_from_stream
    {
        static string we22Input = "000003 123, 456, 789,*3Eabcd000003 123, 456, 789,*3E";
        static string we500Input = "$000003,123, 456, 789,*32\r\nabcd$000003,123, 456, 789,*32\r\n";

        static Stream we22Stream, we500Stream;
        static string firstWE22Line, secondWE22Line;
        static string firstWE500Line, secondWE500Line;

        Establish context = () =>
        {
            we22Stream = new MemoryStream(Encoding.ASCII.GetBytes(we22Input));
            we500Stream = new MemoryStream(Encoding.ASCII.GetBytes(we500Input));
        };

        Because of = () =>
        {
            (firstWE22Line, secondWE22Line, _) = TerasakiStreamReader.ReadLine(we22Stream).Take(2).ToList();
            (firstWE500Line, secondWE500Line, _) = TerasakiStreamReader.ReadLine(we500Stream).Take(2).ToList();
        };

        It should_ignore_junk_between_lines_for_we22 = () =>
        {
            firstWE22Line.ShouldEqual("000003 123, 456, 789,*3E");
            secondWE22Line.ShouldEqual("000003 123, 456, 789,*3E");
        };

        It should_ignore_junk_between_lines_for_we500 = () =>
        {
            firstWE500Line.ShouldEqual("000003,123, 456, 789,*32\r");
            secondWE500Line.ShouldEqual("000003,123, 456, 789,*32\r");
        };
    }
}