using System.Xml.Serialization;

namespace net.derpaul.tf
{
    /// <summary>
    /// Configuration settings of device to read the bricklets
    /// </summary>
    public class DeviceConfig : ConfigLoader<DeviceConfig>, IConfigObject
    {
        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            Delay = 5000;
            BrickDaemonIP = "127.0.0.1";
            BrickDaemonPort = 4223;
            PluginPath = "Plugins";
        }

        /// <summary>
        /// Delay between reading/sending values in milliseconds
        /// </summary>
        public int Delay { get; set; }

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
        /// Product name of plugin set in AssemblyInfo.cs
        /// This is hardcoded and not configurable!
        /// </summary>
        [XmlIgnore]
        public string PluginProductName { get; } = "net.derpaul.tf.plugin";
    }
}