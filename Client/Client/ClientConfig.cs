namespace net.derpaul.tf
{
    /// <summary>
    /// Config class for settings related to reading the bricklets
    /// </summary>
    public class ClientConfig : ConfigLoader<ClientConfig>, ConfigSaver
    {
        /// <summary>
        /// For internal delays
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
    }
}