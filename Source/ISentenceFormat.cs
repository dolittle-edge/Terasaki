/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace RaaLabs.TimeSeries.Terasaki
{

    /// <summary>
    /// Defines how a format for a sentence is to be parsed
    /// </summary>
    public interface ISentenceFormat
    {
        /// <summary>
        /// Gets the talker of the sentence
        /// </summary>
        string Frame { get; }

        /// <summary>
        /// Gets the setence identifier 
        /// </summary>
        string Record { get; }

        /// <summary>
        /// Parse the sentence values and return an instance of a type representing it
        /// </summary>
        /// <param name="values">The values to parse</param>
        /// <returns>All the results</returns>
        IEnumerable<ParsedResult> Parse(string[] values);
    }
}