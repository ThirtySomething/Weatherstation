using System;

namespace net.derpaul.tf
{
    /// <summary>
    /// Class to read temperature using barometer sensor
    /// </summary>
    public class Temperature : Barometer
    {
        /// <summary>
        /// Measurement unit of sensor
        /// </summary>
        public override string Unit { get; } = "°C";

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

            int temperatureRaw = _Bricklet.GetChipTemperature();
            double temperature = temperatureRaw / 100.0;

            return new Tuple<string, double, string>(Name, temperature, Unit);
        }
    }
}