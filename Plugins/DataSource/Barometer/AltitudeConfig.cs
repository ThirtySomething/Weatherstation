namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Configuration settings of altitude sensor
    /// </summary>
    public class AltitudeConfig : ConfigLoader<AltitudeConfig>, IConfigObject
    {
        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            SortOrder = 5;
        }

        /// <summary>
        /// Sort order for altitude
        /// </summary>
        public int SortOrder { get; set; }
    }
}