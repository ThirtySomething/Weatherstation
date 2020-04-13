using Tinkerforge;

namespace net.derpaul.tf.plugin
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
        /// Delay in milli seconds until next measurement value is read
        /// </summary>
        public override int ReadDelay { get; } = AltitudeConfig.Instance.ReadDelay;

        /// <summary>
        /// Initialize internal TF bricklet
        /// </summary>
        /// <param name="connection">Connection to master brick</param>
        /// <param name="UID">Sensor ID</param>
        public override void Init(IPConnection connection, string UID)
        {
            AltitudeConfig.Instance.ShowConfig();
            base.Init(connection, UID);
        }

        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Altitude or 0.0</returns>
        protected override MeasurementValue RawValue()
        {
            MeasurementValue result = new MeasurementValue(Name, Unit, AltitudeConfig.Instance.SortOrder);

            if (Bricklet != null)
            {
                int altitudeRaw = Bricklet.GetAltitude();
                result.Value = altitudeRaw / 100.0;
            }

            return result;
        }
    }
}