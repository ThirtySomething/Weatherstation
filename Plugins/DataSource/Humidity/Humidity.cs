using System.Collections.Generic;
using Tinkerforge;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Class to read values from humidity sensor
    /// </summary>
    public class Humidity : DataSourceBase
    {
        /// <summary>
        /// Measurement unit of sensor
        /// </summary>
        public override string Unit { get; } = "%";

        /// <summary>
        /// Internal object of TF bricklet
        /// </summary>
        private static BrickletHumidity Bricklet { get; set; }

        /// <summary>
        /// The TF sensor type
        /// </summary>
        public override int SensorType { get; } = BrickletHumidity.DEVICE_IDENTIFIER;

        /// <summary>
        /// Delay in milli seconds until next measurement value is read
        /// </summary>
        public override int ReadDelay { get; } = HumidityConfig.Instance.ReadDelay;

        /// <summary>
        /// Flags successful initialization
        /// </summary>
        public override bool IsInitialized { get; set; }

        /// <summary>
        /// Initialize internal TF bricklet
        /// </summary>
        /// <param name="connection">Connection to master brick</param>
        /// <param name="UID">Sensor ID</param>
        /// <returns>true on successful init</returns>
        public override bool Init(IPConnection connection, string UID)
        {
            HumidityConfig.Instance.ShowConfig();
            if (Bricklet == null)
            {
                Bricklet = new BrickletHumidity(UID, connection);
            }
            return true;
        }

        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Humidity or 0.0</returns>
        protected override List<MeasurementValue> RawValues()
        {
            var result = new List<MeasurementValue>();
            MeasurementValue value = new MeasurementValue(Name, "", Unit, HumidityConfig.Instance.SortOrder);

            if (Bricklet != null)
            {
                int humidityRaw = Bricklet.GetHumidity();
                value.Value = humidityRaw / 10.0;
                result.Add(value);
            }

            return result;
        }
    }
}