namespace net.derpaul.tf
{
    /// <summary>
    /// Config class for settings related to reading the bricklets
    /// </summary>
    public class ClientConfig : ConfigLoader<ClientConfig>, IConfigSaver
    {
        /// <summary>
        /// Delay between reading/sending values in milliseconds
        /// </summary>
        public int Delay { get; set; } = 5000;

        /// <summary>
        /// IP address TF brick daemon to connect to
        /// </summary>
        public string BrickDaemonIP { get; set; } = "localhost";

        /// <summary>
        /// Port address TF brick daemon to connect to
        /// </summary>
        public int BrickDaemonPort { get; set; } = 4223;

        /// <summary>
        /// Default path for plugins
        /// </summary>
        public string PluginPath { get; set; } = "Plugins";

        /// <summary>
        /// Product name of plugin set in AssemblyInfo.cs
        /// </summary>
        public string PluginProductName { get; set; } = "net.derpaul.tf.plugin";
    }
}