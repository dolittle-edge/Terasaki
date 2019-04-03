/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Edge.Terasaki
{
    /// <summary>
    /// Represents an implementation for <see cref="IConnector"/>
    /// </summary>
    public class Connector : IConnector
    {
        /// <inheritdoc/>
        public void Subscribe(Action<Channel> subscriber)
        {
            throw new NotImplementedException();
        }
    }
}