namespace net.derpaul.tf
{
    /// <summary>
    /// Configuration settings of humidity sensor
    /// </summary>
    public class HumidityConfig : ConfigLoader<HumidityConfig>, IConfigObject
    {
        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            PluginDelay = TFUtils.DefaultDelay;
            SortOrder = 3;
        }

        /// <summary>
        /// Sort order
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Inidividual delay time of each plugin.
        /// </summary>
        public int PluginDelay { get; set; }
    }
}