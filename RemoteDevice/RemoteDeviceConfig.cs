using System.Xml.Serialization;

namespace net.derpaul.tf
{
    /// <summary>
    /// Configuration settings of remotedevice reading data via MQTT
    /// </summary>
    public class RemoteDeviceConfig : ConfigLoader<RemoteDeviceConfig>, IConfigObject
    {
        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            PluginDelay = TFUtils.DefaultDelay;
            Delay = 1000;
            PluginPath = "Plugins";
            BrokerIP = "test.mosquitto.org";
            BrokerPort = 1883;
            ClientID = "WeatherMQTTRemoteDevice";
            TopicData = "/tinkerforge/weatherstation/dta";
            TopicAcknowledge = "/tinkerforge/weatherstation/ack";
            Handshake = false;
        }

        /// <summary>
        /// Delay between reading/sending values in milliseconds
        /// </summary>
        public int Delay { get; set; }

        /// <summary>
        /// Default path for plugins
        /// </summary>
        public string PluginPath { get; set; }

        /// <summary>
        /// IP of MQTT broker to connect to
        /// </summary>
        public string BrokerIP { get; set; }

        /// <summary>
        /// Port of MQTT broker to connect to
        /// </summary>
        public int BrokerPort { get; set; }

        /// <summary>
        /// Client ID
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Topic to publish data to
        /// </summary>
        public string TopicData { get; set; }

        /// <summary>
        /// Topic to send handshake information
        /// </summary>
        public string TopicAcknowledge { get; set; }

        /// <summary>
        /// To perform some kind of handshake, set to true
        /// </summary>
        public bool Handshake { get; set; }

        /// <summary>
        /// Inidividual delay time of each plugin.
        /// </summary>
        public int PluginDelay { get; set; }

        /// <summary>
        /// Product name of plugin set in AssemblyInfo.cs
        /// This is hardcoded and not configurable!
        /// </summary>
        [XmlIgnore]
        public string PluginProductName { get; } = "net.derpaul.tf.plugin";
    }
}