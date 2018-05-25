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
            SortOrder = 2;
        }

        /// <summary>
        /// Sort order
        /// </summary>
        public int SortOrder { get; set; }
    }
}