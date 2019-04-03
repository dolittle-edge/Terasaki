/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Dolittle.Edge.Modules;
using Dolittle.Edge.Modules.Booting;
using Newtonsoft.Json;

namespace Dolittle.Edge.Terasaki
{
    class Program
    {
        static void Main(string[] args)
        {
            var stream = File.OpenRead("../../sample/terasaki.sample");
            var parser = new Parser();
            parser.BeginParse(stream, channel => {
                var dataPoint = new TagDataPoint<ChannelValue>
                {
                    System = "Terasaki",
                    Tag = channel.Id.ToString(),
                    Value = channel.Value,
                    Timestamp = Timestamp.UtcNow
                };

                var json = JsonConvert.SerializeObject(dataPoint, Formatting.None);
                Console.WriteLine(json);
            });
            stream.Close();
            
            //Bootloader.Configure(_ => {}).Start().Wait();
        }
    }
}