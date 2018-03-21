namespace net.derpaul.tf
{
    /// <summary>
    /// Config class for settings related to reading the bricklets
    /// </summary>
    public class ServerConfig : ConfigLoader<ServerConfig>, ConfigSaver
    {
        /// <summary>
        /// Delay between reading/sending values in milliseconds
        /// </summary>
        public int Delay { get; set; } = 1000;

        /// <summary>
        /// Default path for plugins
        /// </summary>
        public string PluginPath { get; set; } = "Plugins";

        /// <summary>
        /// IP of MQTT broker to connect to
        /// </summary>
        public string BrokerIP { get; set; } = "127.0.0.1";

        /// <summary>
        /// Client ID
        /// </summary>
        public string MQTTClientIDServer { get; set; } = "WeatherMQTTServer";

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