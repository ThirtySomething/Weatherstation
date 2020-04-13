namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Configuration settings of air pressure sensor
    /// </summary>
    public class AirPressureConfig : ConfigLoader<AirPressureConfig>, IConfigObject
    {
        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            PluginDelay = TFUtils.DefaultDelay;
            SortOrder = 2;
        }

        /// <summary>
        /// Sort order for air pressure
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Inidividual delay time of each plugin.
        /// </summary>
        public int PluginDelay { get; set; }
    }
}