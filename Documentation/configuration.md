---
title: Configuration
description: Terasaki Connector Configuration
keywords: Terasaki, Configuration
author: einari
---
The Terasaki needs to configured to point at the correct endpoint consisting of
an IP-address and a TCP port.
During deployment through a [Microsoft IoT Hub Edge deployment](https://docs.microsoft.com/en-us/azure/iot-edge/module-composition)
you'll have to configure the endpoint correctly for it to work.

You'll need to configure the [module twin](https://docs.microsoft.com/en-us/azure/iot-hub/iot-hub-devguide-module-twins)
with the desired property. Inside this you'll have to have a property called connector,
it represents a JSON serialized version of the
[configuration object](https://github.com/dolittle-timeseries/Terasaki/blob/master/Source/ConnectorConfiguration.cs),
lower camel-cased.

```json
{
    "properties": {
        "desired": {
            "connector": {
                "ip": "[ip address]",
                "port": 2101
            }
        }
    }
}
```
