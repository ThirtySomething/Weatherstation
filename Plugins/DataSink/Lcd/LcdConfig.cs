namespace net.derpaul.tf.plugin
{
    public class LcdConfig : ConfigLoader<LcdConfig>, IConfigSaver
    {
        /// <summary>
        /// IP address TF brick daemon to connect to
        /// </summary>
        public string BrickDaemonIP { get; set; } = "127.0.0.1";

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