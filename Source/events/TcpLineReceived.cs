// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RaaLabs.Edge.Modules.EventHandling;

namespace RaaLabs.Edge.Connectors.Terasaki.Events
{
    /// <summary>
    /// Implements event for TcpLineReceived 
    /// </summary>
    public class TcpLineReceived : IEvent
    {
        /// <summary>
        /// gets and sets the Terasaki sentence. 
        /// </summary>
        public string Sentence { get; set; }

        /// <inheritdoc/>
        public TcpLineReceived(string sentence)
        {
            Sentence = sentence;
        }
        public static implicit operator TcpLineReceived(string sentence) => new TcpLineReceived(sentence);
    }
}
