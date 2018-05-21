using System.Xml.Serialization;

namespace net.derpaul.tf
{
    /// <summary>
    /// Configuration settings of server reading data via MQTT
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
        public string PluginPath { get; set; } = "Plugins";

        /// <summary>
        /// Product name of plugin set in AssemblyInfo.cs
        /// This is hardcoded and not configurable!
        /// </summary>
        [XmlIgnore]
        public string PluginProductName { get; } = "net.derpaul.tf.plugin";

        /// <summary>
        /// IP of MQTT broker to connect to
        /// </summary>
        public string BrokerIP { get; set; } = "test.mosquitto.org";

        /// <summary>
        /// Client ID
        /// </summary>
        public string ClientID { get; set; } = "WeatherMQTTServer";

        /// <summary>
        /// Topic to publish data to
        /// </summary>
        public string TopicData { get; set; } = "/tinkerforge/weatherstation/dta";

        /// <summary>
        /// Topic to send handshake information
        /// </summary>
        public string TopicAcknowledge { get; set; } = "/tinkerforge/weatherstation/ack";
    }
}