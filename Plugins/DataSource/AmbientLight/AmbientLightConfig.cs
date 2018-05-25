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
            SortOrder = 3;
        }

        /// <summary>
        /// Sort order
        /// </summary>
        public int SortOrder { get; set; }
    }
}