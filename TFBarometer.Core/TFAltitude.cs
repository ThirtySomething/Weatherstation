namespace net.derpaul.tf
{
    /// <summary>
    /// Class to read altitude from barometer sensor
    /// </summary>
    public class TFAltitude : TFBarometer
    {
        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Altitude or 0.0</returns>
        protected override double ValueGetRaw()
        {
            if (_BrickletBarometer == null)
            {
                return 0.0;
            }

            int altitudeRaw = _BrickletBarometer.GetAltitude();
            double altitude = altitudeRaw / 100.0;

            return altitude;
        }
    }
}