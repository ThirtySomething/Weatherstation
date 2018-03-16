namespace net.derpaul.tf
{
    /// <summary>
    /// Config settings of air pressure sensor
    /// </summary>
    public class AirPressureConfig : ConfigLoader<AirPressureConfig>, ConfigSaver
    {
        /// <summary>
        /// Sort order for air pressure
        /// </summary>
        public int SortOrder = 1;
    }
}