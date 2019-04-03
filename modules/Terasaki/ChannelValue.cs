/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace Dolittle.Edge.Terasaki
{
    /// <summary>
    /// Represents the actual value on the <see cref="ChannelValue"/>
    /// </summary>
    public class ChannelValue
    {
        /// <summary>
        /// Gets or sets the value from the sensor
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Gets or sets the state from the sensor
        /// </summary>
        public char State { get; set; }
    }
}