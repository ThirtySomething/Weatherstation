namespace net.derpaul.tf
{
    /// <summary>
    /// Class to read air pressure from barometer sensor
    /// </summary>
    public class TFAirPressure : TFBarometer
    {
        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Air pressure or 0.0</returns>
        protected override double ValueGetRaw()
        {
            if (_BrickletBarometer == null)
            {
                return 0.0;
            }

            int airPressureRaw = _BrickletBarometer.GetAirPressure();
            double airPressure = airPressureRaw / 1000.0;

            return airPressure;
        }
    }
}