// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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