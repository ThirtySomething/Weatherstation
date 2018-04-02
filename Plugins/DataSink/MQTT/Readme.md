## MQTT

The MQTT plugin publishes the measurement values via MQTT.

Return to [main](./../Readme.md).

## MQTTConfig.config

For the MQTT plugin the following settings are available:

* <code>BrokerIP</code> - The IP of the system where the MQTT broker is running. The default is: <code>localhost</code>
* <code>ClientID</code> - The client ID signaled to the broker, default is: <code>WeatherMQTTClient</code>
* <code>TopicData</code> - The MQTT message topic where to publish measurement values to, by default <code>/tinkerforge/weatherstation/data</code>
* <code>TopicAcknowledge</code> - The MQTT message topic to subscribe to. The server will publish a successful recieve on this topic. Default is <code>/tinkerforge/weatherstation/ack</code>