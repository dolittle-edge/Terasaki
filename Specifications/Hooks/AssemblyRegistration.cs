using RaaLabs.Edge.Testing;
using TechTalk.SpecFlow;

namespace RaaLabs.Edge.Connectors.Terasaki.Specs.Drivers
{
    [Binding]
    class AssemblyRegistration
    {
        private readonly ComponentAssemblies _assemblies;

        public AssemblyRegistration(ComponentAssemblies assemblies)
        {
            _assemblies = assemblies;
        }

        [BeforeScenario]
        private void RegisterAssembly()
        {
            _assemblies.Add(GetType().Assembly);
        }
    }
}