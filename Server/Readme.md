## Server

The server is responsible for:

* Picking up data via MQTT
* Push received data to [data sinks](./../Plugins/DataSink/Readme.md)

__To abort the endless loop of picking measurement values just hit escape.__

Return to [main](./../Readme.md).

## ServerConfig.config

For more details how to configure the plugins see the Readme in the project of the plugin. For the server the following settings are available:

* <code>Delay</code> - time interval at which the next keypress is checked
* <code>PluginPath</code> - The path where the client is looking for datasource and datasink plugins, by default in a subdirectory called <code>Plugins</code>
* <code>PluginProductName</code> - Each plugin should have a specified product name to identify them as plugins for this software and to ignore non-plugin DLL files. The default is <code>net.derpaul.tf.plugin.[name]</code> and this is valid __FOR ALL__ plugins.
* <code>BrokerIP</code> - The IP of the system where the MQTT broker is running. The default is: <code>localhost</code>
* <code>ClientID</code> - The client ID signaled to the broker, default is: <code>WeatherMQTTClient</code>
* <code>TopicData</code> - The MQTT message topic to subscribe where the client publishs the measurement values, by default <code>/tinkerforge/weatherstation/data</code>
* <code>TopicAcknowledge</code> - The MQTT message topic to publish a successful recieve of a measurement value. Default is <code>/tinkerforge/weatherstation/ack</code>

## Remarks

See also the [client documentation](./../Client/Readme.md)
