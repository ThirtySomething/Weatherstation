namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Configuration settings of Lcd bricklet of TF weatherstation
    /// </summary>
    public class LcdConfig : ConfigLoader<LcdConfig>, IConfigObject
    {
        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            BrickDaemonIP = "127.0.0.1";
            BrickDaemonPort = 4223;
            TimestampFormat = "dd.MM.yyyy  HH:mm:ss";
            SkipIndex = 100;
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
        /// Format to display a timestamp
        /// </summary>
        public string TimestampFormat { get; set; }

        /// <summary>
        /// Skip values using this index
        /// </summary>
        public int SkipIndex { get; set; }
    }
}