using RaaLabs.Edge.Modules.EventHandling;

namespace RaaLabs.Edge.Connectors.Terasaki.Events
{
    public class TcpLineReceived : IEvent
    {
        public string Sentence { get; set; }

        public TcpLineReceived(string sentence)
        {
            Sentence = sentence;
        }
        public static implicit operator TcpLineReceived(string sentence) => new TcpLineReceived(sentence);
    }
}
