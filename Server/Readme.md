# Server

The server is the receiver of the transmitted measurement values and has no direct connection to the weatherstation. In detail the server is responsible for:

* Picking up data via MQTT
* Push received data to [data sinks](./../Plugins/DataSink/Readme.md)

__To abort the endless loop of picking measurement values just hit escape.__

Return to [main](./../Readme.md).

## ServerConfig.config

For more details how to configure the plugins see the Readme in the project of the plugin. For the server the following settings are available:

* `Delay` - time interval at which the next keypress is checked
* `PluginPath` - The path where the client is looking for datasource and datasink plugins, by default in a subdirectory called `Plugins`
* `PluginProductName` - Each plugin should have a specified product name to identify them as plugins for this software and to ignore non-plugin DLL files. The default is `net.derpaul.tf.plugin.[name]` and this is valid __FOR ALL__ plugins.
* `BrokerIP` - The IP of the system where the MQTT broker is running. The default is: `127.0.0.1`
* `ClientID` - The client ID signaled to the broker, default is: `WeatherMQTTClient`
* `TopicData` - The MQTT message topic to subscribe where the client publishs the measurement values, by default `/tinkerforge/weatherstation/data`
* `TopicAcknowledge` - The MQTT message topic to publish a successful recieve of a measurement value. Default is `/tinkerforge/weatherstation/ack`

## Remarks

See also the [client documentation](./../Client/Readme.md)
