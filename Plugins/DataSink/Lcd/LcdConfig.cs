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
            PluginDelay = TFUtils.DefaultDelay;
            BrickDaemonIP = "127.0.0.1";
            BrickDaemonPort = 4223;
            TimestampFormat = "dd.MM.yyyy  HH:mm:ss";
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
        /// Inidividual delay time of each plugin.
        /// </summary>
        public int PluginDelay { get; set; }
    }
}