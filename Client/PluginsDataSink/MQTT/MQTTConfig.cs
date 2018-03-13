namespace net.derpaul.tf
{
    public class MQTTConfig : ConfigLoader<MQTTConfig>, ConfigSaver
    {
        /// <summary>
        /// IP of MQTT broker to connect to
        /// </summary>
        public string BrokerIP { get; set; } = "127.0.0.1";

        /// <summary>
        /// Client ID
        /// </summary>
        public string MQTTClientID { get; set; } = "WeatherMQTT";

        /// <summary>
        /// Topic to publish messages to
        /// </summary>
        public string MQTTTopic { get; set; } = "/tinkerforge/weatherstation";
    }
}