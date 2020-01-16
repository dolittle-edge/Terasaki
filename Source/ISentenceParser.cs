/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;

namespace Dolittle.TimeSeries.Terasaki
{
    /// <summary>
    /// Defines the parser of sentences
    /// </summary>
    public interface ISentenceParser
    {
        /// <summary>
        /// Check if sentence can be parsed
        /// </summary>
        /// <param name="sentence">Sentence to check</param>
        /// <returns>True if it can be parsed, false if not</returns>
        bool CanParse(string sentence);
        
        /// <summary>
        /// Parse a parseable sentence into its target object
        /// </summary>
        /// <param name="sentence">Sentence to parse</param>
        /// <returns>All the results parsed</returns>
        IEnumerable<TagWithData> Parse(string sentence);
    }
}