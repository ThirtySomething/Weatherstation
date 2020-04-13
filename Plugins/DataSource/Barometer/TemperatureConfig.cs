namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Configuration settings of temperature sensor
    /// </summary>
    public class TemperatureConfig : ConfigLoader<TemperatureConfig>, IConfigObject
    {
        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            PluginDelay = TFUtils.DefaultDelay;
            SortOrder = 1;
        }

        /// <summary>
        /// Sort order for temperature
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Inidividual delay time of each plugin.
        /// </summary>
        public int PluginDelay { get; set; }
    }
}