// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace RaaLabs.Edge.Connectors.Terasaki
{
    /// <summary>
    /// 
    /// </summary>
    public class TagWithData
    {
        /// <summary>
        /// 
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="data"></param>
        public TagWithData(string tag, dynamic data)
        {
            Tag = tag;
            Data = data;
        }
    }
}
