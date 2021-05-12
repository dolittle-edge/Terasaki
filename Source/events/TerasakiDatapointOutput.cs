using RaaLabs.Edge.Modules.EdgeHub;


namespace RaaLabs.Edge.Connectors.Terasaki.Events
{
    [OutputName("output")]
    public class TerasakiDatapointOutput : IEdgeHubOutgoingEvent
    {
        public string Source { get; set; }

        public string Tag { get; set; }

        public dynamic Value { get; set; }

        public long Timestamp { get; set; }
    }
}
