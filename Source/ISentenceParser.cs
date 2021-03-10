/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;

namespace RaaLabs.Edge.Connectors.Terasaki
{
    /// <summary>
    /// Defines the parser of sentences
    /// </summary>
    public interface ISentenceParser
    {
        /// <summary>
        /// Parse a parseable sentence into its target object
        /// </summary>
        /// <param name="sentence">Sentence to parse</param>
        /// <returns>All the results parsed</returns>
        IEnumerable<TagWithData> Parse(string sentence);
    }
}