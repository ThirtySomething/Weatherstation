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
        protected override Result ValueGetRaw()
        {
            Result result = new Result(Name, Unit);

            if (_Bricklet == null)
            {
                return result;
            }

            int altitudeRaw = _Bricklet.GetAltitude();
            result.Value = altitudeRaw / 100.0;

            return result;
        }
    }
}