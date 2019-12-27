# Remote device

The remote device is the receiver of the transmitted measurement values and has no direct connection to the weatherstation. In detail the remote device is responsible for:

* Picking up data via MQTT
* Push received data to [data sinks](./../Plugins/DataSink/Readme.md)

__To abort the endless loop of picking measurement values just hit escape.__

Return to [main](./../Readme.md).

## RemoteDeviceConfig.config

For more details how to configure the plugins see the Readme in the project of the plugin. For the remote device the following settings are available:

* `Delay` - time interval at which the next keypress is checked.
* `PluginPath` - The path where the client is looking for datasource and datasink plugins, by default in a subdirectory called `Plugins`.
* `BrokerIP` - The IP of the system where the MQTT broker is running. The default is: `test.mosquitto.org`.
* `ClientID` - The client ID signaled to the broker, default is: `WeatherMQTTClient`.
* `TopicData` - The MQTT message topic to subscribe where the client publishs the measurement values, by default `/tinkerforge/weatherstation/data`.
* `TopicAcknowledge` - The MQTT message topic to publish a successful recieve of a measurement value. Default is `/tinkerforge/weatherstation/ack`.
* `Handshake` - If set to true, for each measurement value recieved an acknowledge MQTT message will be sent.

## Remarks

See also the [device documentation](./../Device/Readme.md)
