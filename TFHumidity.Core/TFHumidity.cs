using System;
using Tinkerforge;

namespace net.derpaul.tf
{
    /// <summary>
    /// Class to read values from humidity sensor
    /// </summary>
    public class TFHumidity : TFSensorBase
    {
        /// <summary>
        /// Internal object of TF bricklet
        /// </summary>
        private static BrickletHumidity _BrickletHumidity { get; set; }

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
            if (_BrickletHumidity != null)
            {
                return;
            }

            _BrickletHumidity = new BrickletHumidity(UID, connection);
        }

        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Humidity or 0.0</returns>
        protected override Tuple<string, double> ValueGetRaw()
        {
            if (_BrickletHumidity == null)
            {
                return new Tuple<string, double>(Name, 0.0);
            }

            int humidityRaw = _BrickletHumidity.GetHumidity();
            double humidity = humidityRaw / 10.0;

            return new Tuple<string, double>(Name, humidity);
        }
    }
}