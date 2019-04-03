/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Edge.Terasaki
{
    /// <summary>
    /// Represents an input message
    /// </summary>
    public class TagDataPoint<TValue>
    {
        /// <summary>
        /// Gets or sets the system this value belong to
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// Gets or sets the tag this value belong to
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// Gets or sets the timestamp in the form of EPOCH milliseconds granularity
        /// </summary>
        public long Timestamp { get; set; }
    }
}