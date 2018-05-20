namespace net.derpaul.tf
{
    /// <summary>
    /// Configuration settings of humidity sensor
    /// </summary>
    public class HumidityConfig : ConfigLoader<HumidityConfig>, IConfigSaver
    {
        /// <summary>
        /// Sort order
        /// </summary>
        public int SortOrder = 2;
    }
}