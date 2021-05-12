// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace RaaLabs.Edge.Connectors.Terasaki
{
    /// <summary>
    /// Exception that gets thrown if a sentence has invalid checksum
    /// </summary>
    public class InvalidSentenceChecksumException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InvalidSentenceChecksumException"/>
        /// </summary>
        /// <param name="actualChecksum">The invalid checksum </param>
        /// <param name="expectedChecksum">The expected checksum</param>
        /// <param name="sentence">The invalid sentence</param>
        public InvalidSentenceChecksumException(byte actualChecksum, byte expectedChecksum, string sentence) : base($"Checksum '{actualChecksum}' is invalid, expecting '{expectedChecksum}' for sentence '{sentence}'. Please refer to the standard for Terasaki.")
        {

        }
    }    
}