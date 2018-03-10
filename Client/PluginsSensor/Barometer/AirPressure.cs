using System;

namespace net.derpaul.tf
{
    /// <summary>
    /// Class to read air pressure from barometer sensor
    /// </summary>
    public class AirPressure : Barometer
    {
        /// <summary>
        /// Measurement unit of sensor
        /// </summary>
        public override string Unit { get; } = "mb";

        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Air pressure or 0.0</returns>
        protected override Tuple<string, double, string> ValueGetRaw()
        {
            if (_Bricklet == null)
            {
                return new Tuple<string, double, string>(Name, 0.0, Unit);
            }

            int airPressureRaw = _Bricklet.GetAirPressure();
            double airPressure = airPressureRaw / 1000.0;

            return new Tuple<string, double, string>(Name, airPressure, Unit);
        }
    }
}