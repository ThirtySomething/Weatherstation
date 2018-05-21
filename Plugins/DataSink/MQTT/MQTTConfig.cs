namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Configuration settings of MQTT plugin
    /// </summary>
    public class MQTTConfig : ConfigLoader<MQTTConfig>, IConfigSaver
    {
        /// <summary>
        /// IP of MQTT broker to connect to
        /// </summary>
        public string BrokerIP { get; set; } = "test.mosquitto.org";

        /// <summary>
        /// Client ID
        /// </summary>
        public string ClientID { get; set; } = "WeatherMQTTClient";

        /// <summary>
        /// Topic to publish data to
        /// </summary>
        public string TopicData { get; set; } = "/tinkerforge/weatherstation/dta";

        /// <summary>
        /// Topic to recieve handshake information
        /// </summary>
        public string TopicAcknowledge { get; set; } = "/tinkerforge/weatherstation/ack";

        /// <summary>
        /// Timer interval to check the acknowledge list
        /// </summary>
        public int TimerDelay { get; set; } = 10000;
    }
}