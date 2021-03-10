/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using RaaLabs.Edge;
using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.Edge.Modules.Logging;
using RaaLabs.Edge.Modules.EdgeHub;
using RaaLabs.Edge.Modules.Configuration;
using RaaLabs.Edge.Connectors.Terasaki;

namespace RaaLabs.TimeSeries.Terasaki
{
    class Program
    {
        static void Main(string[] args)
        {
            //Bootloader.Configure(_ => {}).Start().Wait();

            var application = new ApplicationBuilder()
                .WithModule<EventHandling>()
                .WithModule<Logging>()
                .WithModule<Configuration>()
                .WithModule<EdgeHub>()
                .WithType<SentenceParser>()
                .WithTask<TcpConnector>()
                .WithHandler<TerasakiLineHandler>()
                .Build();

            application.Run().Wait();
        }
    }
}