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
            SortOrder = 0;
        }

        /// <summary>
        /// Sort order for temperature
        /// </summary>
        public int SortOrder { get; set; }
    }
}