namespace net.derpaul.tf
{
    /// <summary>
    /// Config class for settings related to reading the bricklets
    /// </summary>
    public class ClientConfig : ConfigLoader<ClientConfig>, ConfigSaver
    {
        /// <summary>
        /// Delay between reading/sending values in milliseconds
        /// </summary>
        public int Delay { get; set; } = 1000;

        /// <summary>
        /// Name of TF host to connect to
        /// </summary>
        public string Host { get; set; } = "localhost";

        /// <summary>
        /// Port of TF host
        /// </summary>
        public int Port { get; set; } = 4223;

        /// <summary>
        /// Default path for plugins
        /// </summary>
        public string PluginPath { get; set; } = "Plugins";
    }
}