using System;

namespace net.derpaul.tf
{
    /// <summary>
    /// Class to read altitude from barometer sensor
    /// </summary>
    public class Altitude : Barometer
    {
        /// <summary>
        /// Measurement unit of sensor
        /// </summary>
        public override string Unit { get; } = "m";

        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Altitude or 0.0</returns>
        protected override Tuple<string, double, string> ValueGetRaw()
        {
            if (_Bricklet == null)
            {
                return new Tuple<string, double, string>(Name, 0.0, Unit);
            }

            int altitudeRaw = _Bricklet.GetAltitude();
            double altitude = altitudeRaw / 100.0;

            return new Tuple<string, double, string>(Name, altitude, Unit);
        }
    }
}