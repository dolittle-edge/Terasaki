// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RaaLabs.Edge.Modules.EventHandling;
using Serilog;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Polly;
using System.Threading.Tasks;

namespace RaaLabs.Edge.Connectors.Terasaki
{
    class TcpConnector : IRunAsync, IProduceEvent<Events.TcpLineReceived>
    {
        private readonly ILogger _logger;
        private readonly ConnectorConfiguration _configuration;
        private readonly int _timeout;
        public event EventEmitter<Events.TcpLineReceived> TcpLineReceived;

        public TcpConnector(ConnectorConfiguration configuration, ILogger logger)
        {
            _logger = logger;
            _configuration = configuration;
            _timeout = int.Parse(Environment.GetEnvironmentVariable("READ_TIMEOUT")?? "60000");
        }

        public async Task Run()
        {
            while (true)
            {
                _logger.Information("Setting up TCP connector");
                var policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryForeverAsync(retryAttempt => TimeSpan.FromSeconds(Math.Min(Math.Pow(2, retryAttempt),3600)),
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
                stream.ReadTimeout = _timeout;
                var reader = TerasakiStreamReader.ReadLineAsync(stream).GetAsyncEnumerator();
                try
                {
                    while (true)
                    {
                        await DoWithTimeout(reader.MoveNextAsync(), _timeout);
                        var sentence = reader.Current;
                        TcpLineReceived(sentence);
                    }
                }
                finally
                {
                    _logger.Information("Done reading TCP stream");
                    socket.Close();
                }
            }
        }

        /// <summary>
        /// Helper method that can handle timeout for ValueTasks.
        /// </summary>
        async ValueTask<T> DoWithTimeout<T>(ValueTask<T> valueTask, int timeout)
        {
            var task = valueTask.AsTask();
            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                return await task;
            }
            else
            {
                throw new OperationCanceledException();
            }
        }
    }
}
