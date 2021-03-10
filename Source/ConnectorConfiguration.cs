/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using RaaLabs.Edge.Modules.Configuration;
using RaaLabs.Edge.Connectors.Terasaki;

namespace RaaLabs.TimeSeries.Terasaki
{
    /// <summary>
    /// Represents the configuration for <see cref="TcpConnector"/>
    /// </summary>
    [Name("Connector.json")]
    public class ConnectorConfiguration : IConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConnectorConfiguration"/>
        /// </summary>
        /// <param name="ip">The IP address for the connector</param>
        /// <param name="port">The Port to connect to</param>
        public ConnectorConfiguration(string ip, int port)
        {
            Ip = ip;
            Port = port;
        }

        /// <summary>
        /// Gets the Ip address that will be used for connecting
        /// </summary>
        public string Ip { get; }

        /// <summary>
        /// Gets the port that will be used for connecting
        /// </summary>
        public int Port { get; }
    }
}