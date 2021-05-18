// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RaaLabs.Edge.Modules.EventHandling;
using Serilog;
using System;
using System.Linq;

namespace RaaLabs.Edge.Connectors.Terasaki
{
    public class TerasakiLineHandler : IConsumeEvent<Events.TcpLineReceived>, IProduceEvent<Events.TerasakiDatapointOutput>
    {
        public event EventEmitter<Events.TerasakiDatapointOutput> SendDatapoint;

        private readonly ILogger _logger;
        private readonly SentenceParser _parser;

        public TerasakiLineHandler(SentenceParser parser, ILogger logger)
        {
            _logger = logger;
            _parser = parser;
        }

        public void Handle(Events.TcpLineReceived @event)
        {
            try
            {
                var tags = _parser.Parse(@event.Sentence).ToList();
                var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                tags.ForEach(tag =>
                {
                    var output = new Events.TerasakiDatapointOutput
                    {
                        Source = "Terasaki",
                        Tag = tag.Tag,
                        Timestamp = timestamp,
                        Value = tag.Data
                    };

                    SendDatapoint(output);
                });
            }
            catch (FormatException ex)
            {
                _logger.Error(ex, $"Trouble parsing  {@event}");
            }

        }
    }
}
