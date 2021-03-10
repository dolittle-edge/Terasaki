using RaaLabs.Edge.Modules.EventHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.Edge.Connectors.Terasaki.events
{
    class TcpLineReceived : IEvent
    {
        public string Value { get; set; }

        public TcpLineReceived(string value)
        {
            Value = value;
        }
        public static implicit operator TcpLineReceived(string value) => new TcpLineReceived(value);
    }
}
