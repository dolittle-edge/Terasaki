/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Sockets;
using RaaLabs.TimeSeries.Modules;
using Dolittle.Logging;
using RaaLabs.TimeSeries.Modules.Connectors;
using System.Threading;

namespace RaaLabs.TimeSeries.Terasaki
{
    /// <summary>
    /// Represents an implementation for <see cref="IAmAStreamingConnector"/>
    /// </summary>
    public class Connector : IAmAStreamingConnector
    {
        /// <inheritdoc/>
        public event DataReceived DataReceived = (tag, ValueTask, timestamp) => { };

        readonly ILogger _logger;
        readonly SentenceParser _sentenceParser;

        readonly ConnectorConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of <see cref="Connector"/>
        /// </summary>
        /// <param name="configuration"><see cref="ConnectorConfiguration"/> holding all configuration</param>
        /// <param name="sentenceParser"><see cref="SentenceParser"/> the sentenceParser to use</param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public Connector(
            ConnectorConfiguration configuration,
            SentenceParser sentenceParser,
            ILogger logger
)
        {
            _logger = logger;
            _configuration = configuration;
            _sentenceParser = sentenceParser;
            
            _logger.Information($"Will connect to '{configuration.Ip}:{configuration.Port}'");
        }

        /// <inheritdoc/>
        public Source Name => "Terasaki";

        /// <inheritdoc/>
        public void Connect()
        {
            while(true)
            {
                try
                {
                    var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(IPAddress.Parse(_configuration.Ip), _configuration.Port);

                    using (var stream = new NetworkStream(socket, FileAccess.Read, true))
                    {
                        foreach(var line in TerasakiStreamReader.ReadLine(stream))
                        {
                            ParseSentence(line);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error while connecting to TCP stream");
                }

                Thread.Sleep(10000);
            }
        }

        void ParseSentence(string sentence)
        {
            try
            {
                System.Collections.Generic.List<TagWithData> output = _sentenceParser.Parse(sentence).ToList();
                output.ForEach(_ =>
                {
                    DataReceived(_.Tag, _.Data, Timestamp.UtcNow);
                    _logger.Information($"Tag: {_.Tag}, Value : {_.Data}");
                });
            }
            catch (FormatException ex)
            {
                _logger.Error(ex, $"Trouble parsing  {sentence}");
            }
        }
    }
}