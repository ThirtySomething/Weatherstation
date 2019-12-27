# MQTT

The MQTT plugin publishes the measurement values via MQTT.

Return to [main](./../Readme.md).

## MQTTConfig.config

For the MQTT plugin the following settings are available:

* `BrokerIP` - The IP of the system where the MQTT broker is running. The default is: `test.mosquitto.org`
* `ClientID` - The client ID signaled to the broker, default is: `WeatherMQTTDevice`
* `TopicData` - The MQTT message topic where to publish measurement values to, by default `/tinkerforge/weatherstation/data`
* `TopicAcknowledge` - The MQTT message topic to subscribe to. The remote device will publish a successful recieve on this topic. Default is `/tinkerforge/weatherstation/ack`
* `Handshake` - If set to true, each measurement value is stored internally until the remote device acknoledged the recievement.
