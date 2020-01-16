/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

namespace RaaLabs.TimeSeries.Terasaki
{
    /// <summary>
    /// Represents a parsed result
    /// </summary>
    public class ParsedResult
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ParsedResult"/>
        /// </summary>
        /// <param name="type">Type string representing the result</param>
        /// <param name="result">The actual result</param>
        public ParsedResult(string type, object result)
        {
            Type = type;
            Result = result;
        }

        /// <summary>
        /// Gets the type string
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets the result
        /// </summary>
        public object Result { get; }
    }
}