/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.TimeSeries.Terasaki
{
    /// <summary>
    /// Represents a Terasaski channel data point
    /// </summary>
    public class Channel
    {
        /// <summary>
        /// Gets or sets the Id the channel belongs to
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ChannelValue"/>
        /// </summary>
        public ChannelValue Value { get; set; }
    }
}