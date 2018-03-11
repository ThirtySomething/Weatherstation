namespace net.derpaul.tf
{
    /// <summary>
    /// Config settings of barometer sensor
    /// </summary>
    public class BarometerConfig : ConfigLoader<BarometerConfig>, ConfigSaver
    {
        /// <summary>
        /// Sort order for air pressure
        /// </summary>
        public int SortOrderAirPressure = 1;

        /// <summary>
        /// Sort order for altitude
        /// </summary>
        public int SortOrderAltitude = 4;

        /// <summary>
        /// Sort order for temperature
        /// </summary>
        public int SortOrderTemperature = 0;
    }
}