using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.TimeSeries.Terasaki;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.Edge.Connectors.Terasaki
{
    class TerasakiLineHandler : IConsumeEvent<events.TcpLineReceived>, IProduceEvent<events.TerasakiDatapointOutput>
    {
        public event EventEmitter<events.TerasakiDatapointOutput> SendDatapoint;

        private readonly ILogger _logger;
        private readonly SentenceParser _parser;

        public TerasakiLineHandler(SentenceParser parser, ILogger logger)
        {
            _logger = logger;
            _parser = parser;
        }

        public void Handle(events.TcpLineReceived @event)
        {
            try
            {
                var tags = _parser.Parse(@event.Value).ToList();
                var timestamp = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                tags.ForEach(tag =>
                {
                    _logger.Information($"Tag: {tag.Tag}, Value : {tag.Data}");

                    var output = new events.TerasakiDatapointOutput
                    {
                        source = "Terasaki",
                        tag = tag.Tag,
                        timestamp = timestamp,
                        value = tag.Data
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
