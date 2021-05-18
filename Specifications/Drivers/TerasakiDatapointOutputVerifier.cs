using TechTalk.SpecFlow;
using FluentAssertions;
using System.Globalization;
using RaaLabs.Edge.Connectors.Terasaki.Events;

namespace RaaLabs.Edge.Connectors.Terasaki.Specs.Drivers
{
    class TerasakiDatapointOutputVerifier : IProducedEventVerifier<TerasakiDatapointOutput>
    {
        public void VerifyFromTableRow(TerasakiDatapointOutput @event, TableRow row)
        {
            float actualValue = @event.Value;
            var expectedValue = float.Parse(row["Value"], CultureInfo.InvariantCulture.NumberFormat);
            @event.Source.Should().Be("Terasaki");
            @event.Tag.Should().Be(row["Tag"]);
            actualValue.Should().BeApproximately(expectedValue, 0.0001f);
        }
    }
}

