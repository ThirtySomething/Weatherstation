using System.Xml.Serialization;

namespace net.derpaul.tf
{
    /// <summary>
    /// Configuration settings of datacollector to read the bricklets
    /// </summary>
    public class DataCollectorConfig : ConfigLoader<DataCollectorConfig>, IConfigObject
    {
        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            BrickDaemonIP = "127.0.0.1";
            BrickDaemonPort = 4223;
            PluginPath = "Plugins";
            Location = "Kochertürn";
        }

        /// <summary>
        /// IP address TF brick daemon to connect to
        /// </summary>
        public string BrickDaemonIP { get; set; }

        /// <summary>
        /// Port address TF brick daemon to connect to
        /// </summary>
        public int BrickDaemonPort { get; set; }

        /// <summary>
        /// Default path for plugins
        /// </summary>
        public string PluginPath { get; set; }

        /// <summary>
        /// Default location of datacollector
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Product name of plugin set in AssemblyInfo.cs
        /// This is hardcoded and not configurable!
        /// </summary>
        [XmlIgnore]
        public string PluginProductName { get; } = "net.derpaul.tf.plugin";
    }
}