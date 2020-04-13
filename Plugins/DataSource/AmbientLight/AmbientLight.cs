using Tinkerforge;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Class to read values from ambient light sensor
    /// </summary>
    public class AmbientLight : DataSourceBase
    {
        /// <summary>
        /// Measurement unit of sensor
        /// </summary>
        public override string Unit { get; } = "lx";

        /// <summary>
        /// Internal object of TF bricklet
        /// </summary>
        private static BrickletAmbientLight Bricklet { get; set; }

        /// <summary>
        /// Delay until next measurement value is read
        /// </summary>
        public override int ReadDelay { get; } = AmbientLightConfig.Instance.ReadDelay;

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
            AmbientLightConfig.Instance.ShowConfig();
            if (Bricklet == null)
            {
                Bricklet = new BrickletAmbientLight(UID, connection);
            }
        }

        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Illuminance or 0.0</returns>
        protected override MeasurementValue RawValue()
        {
            MeasurementValue result = new MeasurementValue(Name, Unit, AmbientLightConfig.Instance.SortOrder);

            if (Bricklet != null)
            {
                int rawIlluminance = Bricklet.GetIlluminance();
                result.Value = rawIlluminance / 10.0;
            }

            return result;
        }
    }
}