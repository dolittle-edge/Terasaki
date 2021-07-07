// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.Edge.Modules.EdgeHub;
using RaaLabs.Edge.Modules.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace RaaLabs.Edge.Connectors.Terasaki
{
    [ExcludeFromCodeCoverage]
    static class Program
    {
        static void Main(string[] args)
        {
            var application = new ApplicationBuilder()
                .WithModule<EventHandling>()
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