using TechTalk.SpecFlow;
using RaaLabs.Edge.Testing;

namespace RaaLabs.Edge.Connectors.Terasaki.Specs.Drivers
{

    [Binding]
    public sealed class TypeMapperProvider
    {
        private readonly TypeMapping _typeMapping;

        public TypeMapperProvider(TypeMapping typeMapping)
        {
            _typeMapping = typeMapping;
        }

        [BeforeScenario]
        public void SetupTypes()
        {
            _typeMapping.Add("TerasakiLineHandler", typeof(TerasakiLineHandler));
            _typeMapping.Add("TcpLineReceived", typeof(Events.TcpLineReceived));
            _typeMapping.Add("TerasakiDatapointOutput", typeof(Events.TerasakiDatapointOutput));
        }
    }
}


