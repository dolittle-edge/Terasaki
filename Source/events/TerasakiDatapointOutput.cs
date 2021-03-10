using RaaLabs.Edge.Modules.EdgeHub;
using RaaLabs.Edge.Modules.EventHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.Edge.Connectors.Terasaki.events
{
    [OutputName("output")]
    class TerasakiDatapointOutput : IEdgeHubOutgoingEvent
    {
        public string source { get; set; }

        public string tag { get; set; }

        public dynamic value { get; set; }

        public long timestamp { get; set; }
    }
}
