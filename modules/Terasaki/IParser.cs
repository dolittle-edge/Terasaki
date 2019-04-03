/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;

namespace Dolittle.Edge.Terasaki
{
    /// <summary>
    /// Defines the parser that is capable of parsing the data coming from Terasaki
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Begin parsing from a continuous <see cref="Stream"/>
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to continuously get from</param>
        /// <param name="callback"><see cref="Action{Channel}">Callback</see> for each <see cref="Channel"/> parsed</param>
        void BeginParse(Stream stream, Action<Channel> callback);
    }
}