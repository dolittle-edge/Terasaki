/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace Dolittle.Edge.Terasaki
{
    /// <summary>
    /// Defines the connector that is responsible for connecting and keeping it with Terasaki
    /// </summary>
    public interface IConnector
    {
        /// <summary>
        /// Starts the connector to begin streaming data
        /// </summary>
        void Start();

        /// <summary>
        /// Subscribe to <see cref="Channel"/> values coming
        /// </summary>
        /// <param name="subscriber">The subscriber method</param>
        void Subscribe(Action<Channel> subscriber);
    }
}