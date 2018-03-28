namespace net.derpaul.tf
{
    /// <summary>
    /// Config settings of barometer sensor
    /// </summary>
    public class TemperatureConfig : ConfigLoader<TemperatureConfig>, IConfigSaver
    {
        /// <summary>
        /// Sort order for temperature
        /// </summary>
        public int SortOrder = 0;
    }
}