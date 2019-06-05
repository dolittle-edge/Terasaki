/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.TimeSeries.Modules;
using Dolittle.Logging;

namespace Dolittle.TimeSeries.Terasaki
{
    /// <summary>
    /// Represents an implementation for <see cref="IConnector"/>
    /// </summary>
    public class Connector : IConnector
    {
        readonly ILogger _logger;
        readonly IParser _parser;
        readonly ConcurrentBag<Action<Channel>> _subscribers;
        private readonly ConnectorConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of <see cref="Connector"/>
        /// </summary>
        /// <param name="configuration"><see cref="ConnectorConfiguration"/> holding all configuration</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        /// <param name="parser"><see cref="IParser"/> for dealing with the actual parsing</param>
        public Connector(
            ConnectorConfiguration configuration,
            ILogger logger,
            IParser parser)
        {
            _logger = logger;
            _parser = parser;
            _subscribers = new ConcurrentBag<Action<Channel>>();
            _configuration = configuration;
            _logger.Information($"Will connect to '{configuration.Ip}:{configuration.Port}'");
        }

        /// <inheritdoc/>
        public void Start()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                        socket.Connect(IPAddress.Parse(_configuration.Ip), _configuration.Port);

                        using (var stream = new NetworkStream(socket, FileAccess.Read, true))
                        {
                            _parser.BeginParse(stream, channel =>
                            {
                                var dataPoint = new TagDataPoint<ChannelValue>
                                {
                                    ControlSystem = "Terasaki",
                                    Tag = channel.Id.ToString(),
                                    Value = channel.Value,
                                    Timestamp = Timestamp.UtcNow
                                };
                                _subscribers.ForEach(subscriber => subscriber(channel));
                            });
                        }

                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex, "Error while connecting to TCP stream");
                    }
                    
                    Thread.Sleep(10000);
                }
            });
        }

        /// <inheritdoc/>
        public void Subscribe(Action<Channel> subscriber)
        {
            _subscribers.Add(subscriber);
        }
    }
}