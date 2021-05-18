# Connectors.Terasaki

[![.NET 5.0](https://github.com/RaaLabs/Connectors.Terasaki/actions/workflows/dotnet-core.yml/badge.svg)](https://github.com/RaaLabs/Connectors.Terasaki/actions/workflows/dotnet-core.yml)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=RaaLabs_TimeSeries.Terasaki&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=RaaLabs_TimeSeries.Terasaki)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=RaaLabs_TimeSeries.Terasaki&metric=coverage)](https://sonarcloud.io/dashboard?id=RaaLabs_TimeSeries.Terasaki)

This document describes the Connectors.Terasaki module for RaaLabs Edge.

## What does it do?

Terasaki receives sentences over TCP. Each sentence contains a block id represented by the first 3 places at the start of the sentence, the 3 last represents the number of values contained in the block. `001003` represents block id 1 and it contains 3 values. The following comma-separated places are representing the values. The sentence is finalized with a checksum and a new line.

The connector are producing events of type [OutputName("output")] and should be routed to [IdentityMapper](https://github.com/RaaLabs/IdentityMapper).

## Configuration

The module is configured using a JSON file. `connector.json` represents the connection to the TCP or UDP stream using IP and port.

```json
{
    "ip": "127.0.0.1",
    "port": 4001
}
```

## IoT Edge Deployment

### $edgeAgent

In your `deployment.json` file, you will need to add the module. For more details on modules in IoT Edge, go [here](https://docs.microsoft.com/en-us/azure/iot-edge/module-composition).

The module has persistent state and it is assuming that this is in the `config` folder relative to where the binary is running.
Since this is running in a containerized environment, the state is not persistent between runs. To get this state persistent, you'll
need to configure the deployment to mount a folder on the host into the data folder.

In your `deployment.json` file where you added the module, inside the `HostConfig` property, you should add the volume binding.

```json
 "Mounts": [
    {
        "Type": "volume",
        "Source": "raalabs-config-terasaki",
        "Target": "/app/config",
        "RW": false
    }]}
```

```json
{
    "modulesContent": {
        "$edgeAgent": {
            "properties.desired.modules.Terasaki": {
                "settings": {
                    "image": "<repo-name>/connectors-terasaki:<tag>",
                    "createOptions": {
                        "HostConfig": {
                            "Mounts": [
                                {
                                    "Type": "volume",
                                    "Source": "raalabs-config-terasaki",
                                    "Target": "/app/config",
                                    "RW": false
                                }]}
                },
                "type": "docker",
                "version": "1.0",
                "status": "running",
                "restartPolicy": "always"
            }
        }
    }
}
```

### $edgeHub

The routes in edgeHub can be specified like the example below.

```json
{
    "$edgeHub": {
        "properties.desired.routes.TerasakiToIdentityMapper": "FROM /messages/modules/Terasaki/outputs/output INTO BrokeredEndpoint(\"/modules/IdentityMapper/inputs/events\")",
    }
}
```
