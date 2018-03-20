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

        /// <summary>
        /// Format to display a timestamp
        /// </summary>
        public string TimestampFormat = "dd.MM.yyyy  HH:mm:ss";
    }
}