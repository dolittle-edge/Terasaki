/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace Dolittle.Edge.Terasaki
{
    /// <summary>
    /// Represents an implementation for <see cref="IConnector"/>
    /// </summary>
    public class Connector : IConnector
    {
        List<Action<Channel>>   _subscribers = new List<Action<Channel>>();

        /// <summary>
        /// Initializes a new instance of <see cref="Connector"/>
        /// </summary>
        /// <param name="parser"><see cref="IParser"/> for dealing with the actual parsing</param>
        public Connector(IParser parser)
        {
            // TODO: Socket magic

            parser.BeginParse(null, channel => {
                _subscribers.ForEach(subscriber => subscriber(channel));
            });
        }


        /// <inheritdoc/>
        public void Subscribe(Action<Channel> subscriber)
        {
            _subscribers.Add(subscriber);
        }
    }
}