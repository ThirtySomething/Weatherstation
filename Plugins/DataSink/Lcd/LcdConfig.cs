namespace net.derpaul.tf
{
    public class LcdConfig : ConfigLoader<LcdConfig>, IConfigSaver
    {
        /// <summary>
        /// IP address TF brick daemon to connect to
        /// </summary>
        public string BrickDaemonIP { get; set; } = "localhost";

        /// <summary>
        /// Port address TF brick daemon to connect to
        /// </summary>
        public int BrickDaemonPort { get; set; } = 4223;

        /// <summary>
        /// Format to display a timestamp
        /// </summary>
        public string TimestampFormat = "dd.MM.yyyy  HH:mm:ss";
    }
}