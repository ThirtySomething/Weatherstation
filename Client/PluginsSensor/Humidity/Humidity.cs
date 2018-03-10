using System;
using Tinkerforge;

namespace net.derpaul.tf
{
    /// <summary>
    /// Class to read values from humidity sensor
    /// </summary>
    public class Humidity : SensorBase
    {
        /// <summary>
        /// Measurement unit of sensor
        /// </summary>
        public override string Unit { get; } = "%";

        /// <summary>
        /// Internal object of TF bricklet
        /// </summary>
        private static BrickletHumidity _Bricklet { get; set; }

        /// <summary>
        /// The TF sensor type
        /// </summary>
        public override int SensorType { get; } = BrickletHumidity.DEVICE_IDENTIFIER;

        /// <summary>
        /// Initialize internal TF bricklet
        /// </summary>
        /// <param name="connection">Connection to master brick</param>
        /// <param name="UID">Sensor ID</param>
        public override void Init(IPConnection connection, string UID)
        {
            if (_Bricklet != null)
            {
                return;
            }

            _Bricklet = new BrickletHumidity(UID, connection);
        }

        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Humidity or 0.0</returns>
        protected override Tuple<string, double, string> ValueGetRaw()
        {
            if (_Bricklet == null)
            {
                return new Tuple<string, double, string>(Name, 0.0, Unit);
            }

            int humidityRaw = _Bricklet.GetHumidity();
            double humidity = humidityRaw / 10.0;

            return new Tuple<string, double, string>(Name, humidity, Unit);
        }
    }
}