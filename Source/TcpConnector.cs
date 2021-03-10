using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.TimeSeries.Terasaki;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Polly;
using System.Threading.Tasks;

namespace RaaLabs.Edge.Connectors.Terasaki
{
    class TcpConnector : IRunAsync, IProduceEvent<events.TcpLineReceived>
    {
        private readonly ILogger _logger;
        private readonly ConnectorConfiguration _configuration;

        public event EventEmitter<events.TcpLineReceived> TcpLineReceived;

        public TcpConnector(ConnectorConfiguration configuration, ILogger logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task Run()
        {
            while(true)
            {
                _logger.Information("Setting up TCP connector");
                var policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryForeverAsync(retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    (exception, timeSpan, context) =>
                    {
                        _logger.Error(exception, $"Terasaki connector threw an exception during connect - retrying");
                    });

                await policy.ExecuteAsync(async () =>
                {
                    await Connect();
                });

                await Task.Delay(1000);
            }
        }

        private async Task Connect()
        {
            IPAddress address = IPAddress.Parse(_configuration.Ip);
            int port = _configuration.Port;

            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(address, port);

            using (var stream = new NetworkStream(socket, FileAccess.Read, true))
            {
                stream.ReadTimeout = 30_000;
                var reader = TerasakiStreamReader.ReadLineAsync(stream).GetAsyncEnumerator();
                while (await reader.MoveNextAsync())
                {
                    var line = reader.Current;
                    TcpLineReceived(line);
                }
            }

            _logger.Information("Done reading TCP stream");
            socket.Close();
        }
    }
}
