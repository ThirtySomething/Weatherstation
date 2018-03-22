namespace net.derpaul.tf
{
    /// <summary>
    /// Configuration settings for MQTT
    /// </summary>
    public class MQTTConfig : ConfigLoader<MQTTConfig>, IConfigSaver
    {
        /// <summary>
        /// IP of MQTT broker to connect to
        /// </summary>
        public string BrokerIP { get; set; } = "127.0.0.1";

        /// <summary>
        /// Client ID
        /// </summary>
        public string MQTTClientIDClient { get; set; } = "WeatherMQTTClient";

        /// <summary>
        /// Topic to publish data to
        /// </summary>
        public string MQTTTopicData { get; set; } = "/tinkerforge/weatherstation/data";

        /// <summary>
        /// Topic to recieve handshake information
        /// </summary>
        public string MQTTTopicHandshake { get; set; } = "/tinkerforge/weatherstation/handshake";
    }
}