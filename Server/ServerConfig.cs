namespace net.derpaul.tf
{
    /// <summary>
    /// Config class for settings related to reading the bricklets
    /// </summary>
    public class ServerConfig : ConfigLoader<ServerConfig>, IConfigSaver
    {
        /// <summary>
        /// Delay between reading/sending values in milliseconds
        /// </summary>
        public int Delay { get; set; } = 1000;

        /// <summary>
        /// Default path for plugins
        /// </summary>
        public string PluginPath { get; set; } = "";

        /// <summary>
        /// IP of MQTT broker to connect to
        /// </summary>
        public string BrokerIP { get; set; } = "127.0.0.1";

        /// <summary>
        /// Client ID
        /// </summary>
        public string ClientID { get; set; } = "WeatherMQTTServer";

        /// <summary>
        /// Topic to publish data to
        /// </summary>
        public string TopicData { get; set; } = "/tinkerforge/weatherstation/data";

        /// <summary>
        /// Topic to send handshake information
        /// </summary>
        public string TopicAcknowledge { get; set; } = "/tinkerforge/weatherstation/ack";
    }
}