namespace net.derpaul.tf
{
    /// <summary>
    /// Config settings for humidity sensor
    /// </summary>
    public class HumidityConfig : ConfigLoader<HumidityConfig>, ConfigSaver
    {
        /// <summary>
        /// Sort order
        /// </summary>
        public int SortOrder = 2;
    }
}