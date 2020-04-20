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
            ReadDelay = CommonUtils.DefaultDelay;
            SortOrder = 3;
        }

        /// <summary>
        /// Delay in milli seconds until next measurement value is read
        /// </summary>
        public int ReadDelay { get; set; }

        /// <summary>
        /// Sort order
        /// </summary>
        public int SortOrder { get; set; }
    }
}