/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dolittle.Collections;
using Dolittle.Edge.Modules;
using Dolittle.Logging;
using Dolittle.Serialization.Json;

namespace Dolittle.Edge.Terasaki
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public IEnumerable<Message> Messages { get; set; }
        
    }


    /// <summary>
    /// 
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string Owner { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TagId {  get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset Timestamp {  get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object Value {  get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TagDataPoint
    {
        /// <summary>
        /// 
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long Timestamp { get; set; }
    }


    /// <summary>
    /// Represents a <see cref="ICanHandle{T}"/> for storing messages offline
    /// </summary>
    public class Handler : ICanHandle<MessageContainer>
    {
        readonly ICommunicationClient _client;
        readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance ofr <see cref="Handler"/>
        /// </summary>
        /// <param name="client"><see cref="ICommunicationClient"/> for communication</param>
        /// <param name="logger"><see cref="ILogger"/> used for logging</param>
        public Handler(ICommunicationClient client, ILogger logger)
        {
            _client = client;
            _logger = logger;
        }

        /// <inheritdoc/>
        public Input Input => "input";

        /// <inheritdoc/>
        public async Task Handle(MessageContainer container)
        {
            _logger.Information($"Data received");

            container.Messages.Select(_ => new TagDataPoint
            {
                System = _.Owner,
                Tag = _.TagId,
                Value = _.Value,
                Timestamp = _.Timestamp.ToUnixTimeMilliseconds()
            }).ForEach(_ => _client.SendAsJson("output", _));

            await Task.CompletedTask;
        }
    }
}