using System.Collections.Generic;
using Tinkerforge;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Class to read temperature using barometer sensor
    /// </summary>
    public class Temperature : Barometer
    {
        /// <summary>
        /// Measurement unit of sensor
        /// </summary>
        public override string Unit { get; } = "C";

        /// <summary>
        /// Delay in milli seconds until next measurement value is read
        /// </summary>
        public override int ReadDelay { get; } = TemperatureConfig.Instance.ReadDelay;

        /// <summary>
        /// Initialize internal TF bricklet
        /// </summary>
        /// <param name="connection">Connection to master brick</param>
        /// <param name="UID">Sensor ID</param>
        public override void Init(IPConnection connection, string UID)
        {
            TemperatureConfig.Instance.ShowConfig();
            base.Init(connection, UID);
        }

        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Air pressure or 0.0</returns>
        protected override List<MeasurementValue> RawValues()
        {
            var result = new List<MeasurementValue>();
            MeasurementValue value = new MeasurementValue(Name, Unit, TemperatureConfig.Instance.SortOrder);

            if (Bricklet != null)
            {
                int temperatureRaw = Bricklet.GetChipTemperature();
                value.Value = temperatureRaw / 100.0;
                result.Add(value);
            }

            return result;
        }
    }
}