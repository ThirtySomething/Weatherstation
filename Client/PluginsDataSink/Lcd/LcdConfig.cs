namespace net.derpaul.tf
{
    public class LcdConfig : ConfigLoader<LcdConfig>, ConfigSaver
    {
        /// <summary>
        /// Host to connect to
        /// </summary>
        public string Host { get; set; } = "localhost";

        /// <summary>
        /// Port of ´host to connect to
        /// </summary>
        public int Port { get; set; } = 4223;
    }
}