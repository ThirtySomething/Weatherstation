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
            ReadDelay = TFUtils.DefaultDelay;
            SortOrder = 2;
        }

        /// <summary>
        /// Delay in milli seconds until next measurement value is read
        /// </summary>
        public int ReadDelay { get; set; }

        /// <summary>
        /// Sort order for air pressure
        /// </summary>
        public int SortOrder { get; set; }
    }
}