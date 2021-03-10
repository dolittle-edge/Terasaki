using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
