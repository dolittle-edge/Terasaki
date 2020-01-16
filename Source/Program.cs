/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using RaaLabs.TimeSeries.Modules;
using RaaLabs.TimeSeries.Modules.Booting;
using Newtonsoft.Json;

namespace RaaLabs.TimeSeries.Terasaki
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootloader.Configure(_ => {}).Start().Wait();
        }
    }
}