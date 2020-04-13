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
            ReadDelay = TFUtils.DefaultDelay;
            SortOrder = 5;
        }

        /// <summary>
        /// Delay in milli seconds until next measurement value is read
        /// </summary>
        public int ReadDelay { get; set; }

        /// <summary>
        /// Sort order for altitude
        /// </summary>
        public int SortOrder { get; set; }
    }
}