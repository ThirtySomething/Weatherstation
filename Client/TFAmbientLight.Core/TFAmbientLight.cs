using System;
using Tinkerforge;

namespace net.derpaul.tf
{
    /// <summary>
    /// Class to read values from ambient light sensor
    /// </summary>
    public class TFAmbientLight : TFSensorBase
    {
        /// <summary>
        /// Internal object of TF bricklet
        /// </summary>
        private static BrickletAmbientLight _BrickletAmbientLight { get; set; }

        /// <summary>
        /// The TF sensor type
        /// </summary>
        public override int SensorType { get; } = BrickletAmbientLight.DEVICE_IDENTIFIER;

        /// <summary>
        /// Initialize internal TF bricklet
        /// </summary>
        /// <param name="connection">Connection to master brick</param>
        /// <param name="UID">Sensor ID</param>
        public override void Init(IPConnection connection, string UID)
        {
            if (_BrickletAmbientLight != null)
            {
                return;
            }

            _BrickletAmbientLight = new BrickletAmbientLight(UID, connection);
        }

        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Illuminance or 0.0</returns>
        protected override Tuple<string, double> ValueGetRaw()
        {
            if (_BrickletAmbientLight == null)
            {
                return new Tuple<string, double>(Name, 0.0);
            }

            int illuminanceRaw = _BrickletAmbientLight.GetIlluminance();
            double illuminance = illuminanceRaw / 10.0;

            return new Tuple<string, double>(Name, illuminance);
        }
    }
}