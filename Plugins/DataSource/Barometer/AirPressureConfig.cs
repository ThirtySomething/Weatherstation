namespace net.derpaul.tf
{
    /// <summary>
    /// Configuration settings of air pressure sensor
    /// </summary>
    public class AirPressureConfig : ConfigLoader<AirPressureConfig>, IConfigSaver
    {
        /// <summary>
        /// Sort order for air pressure
        /// </summary>
        public int SortOrder = 1;
    }
}