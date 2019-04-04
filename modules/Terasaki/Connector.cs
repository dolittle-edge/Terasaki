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
using Dolittle.Edge.Modules;
using Dolittle.Logging;

namespace Dolittle.Edge.Terasaki
{
    /// <summary>
    /// Represents an implementation for <see cref="IConnector"/>
    /// </summary>
    public class Connector : IConnector
    {
        readonly ILogger _logger;
        readonly IParser _parser;
        readonly ConcurrentBag<Action<Channel>> _subscribers;

        /// <summary>
        /// Initializes a new instance of <see cref="Connector"/>
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        /// <param name="parser"><see cref="IParser"/> for dealing with the actual parsing</param>
        public Connector(ILogger logger, IParser parser)
        {
            _logger = logger;
            _parser = parser;
            _subscribers = new ConcurrentBag<Action<Channel>>();
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
                        socket.Connect(IPAddress.Parse("10.48.52.181"), 2101);

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