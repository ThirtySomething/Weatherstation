namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Configuration settings of ambient light sensor
    /// </summary>
    public class AmbientLightConfig : ConfigLoader<AmbientLightConfig>, IConfigObject
    {
        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            PluginDelay = TFUtils.DefaultDelay;
            SortOrder = 4;
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