using Tinkerforge;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Class to read values from ambient light sensor
    /// </summary>
    public class AmbientLight : SensorBase
    {
        /// <summary>
        /// Measurement unit of sensor
        /// </summary>
        public override string Unit { get; } = "lx";

        /// <summary>
        /// Internal object of TF bricklet
        /// </summary>
        private static BrickletAmbientLight _Bricklet { get; set; }

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
            if (_Bricklet == null)
            {
                _Bricklet = new BrickletAmbientLight(UID, connection);
            }
        }

        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Illuminance or 0.0</returns>
        protected override MeasurementValue ValueGetRaw()
        {
            MeasurementValue result = new MeasurementValue(Name, Unit, AmbientLightConfig.Instance.SortOrder);

            if (_Bricklet != null)
            {
                int rawIlluminance = _Bricklet.GetIlluminance();
                result.Value = rawIlluminance / 10.0;
            }

            return result;
        }
    }
}