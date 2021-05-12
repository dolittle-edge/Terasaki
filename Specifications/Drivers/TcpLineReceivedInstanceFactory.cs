using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using RaaLabs.Edge.Connectors.Terasaki.Events;

namespace RaaLabs.Edge.Connectors.Terasaki.Specs.Drivers
{
    class TcpLineReceivedInstanceFactory : IEventInstanceFactory<TcpLineReceived>
    {
        public TcpLineReceived FromTableRow(TableRow row)
        {
            var sentence = row.CreateInstance<TcpLineReceived>();
            return sentence;
        }
    }
}
