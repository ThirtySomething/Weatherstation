namespace net.derpaul.tf
{
    /// <summary>
    /// Internal class to held some constants
    /// </summary>
    internal class Defines
    {
        /// <summary>
        /// Path to sensor plugins
        /// </summary>
        internal const string constPluginPath = "Plugins";

        /// <summary>
        /// Name of TF host to connect to
        /// </summary>
        internal const string constDefaultHost = "localhost";

        /// <summary>
        /// Port of TF host
        /// </summary>
        internal const int constDefaultPort = 4223;

        /// <summary>
        /// Maximum number of retries to perform reconnect
        /// </summary>
        internal const int constReconnectMax = 5;

        /// <summary>
        /// For internal delays
        /// </summary>
        internal const int constDefaultDelay = 1000;
    }
}