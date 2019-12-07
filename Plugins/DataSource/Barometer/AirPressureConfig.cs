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
            SortOrder = 2;
        }

        /// <summary>
        /// Sort order for air pressure
        /// </summary>
        public int SortOrder { get; set; }
    }
}