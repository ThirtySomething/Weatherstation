using System.Collections.Generic;
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
            AltitudeConfig.Instance.ShowConfig();
            return base.Init(connection, UID);
        }

        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Altitude or 0.0</returns>
        protected override List<MeasurementValue> RawValues()
        {
            var result = new List<MeasurementValue>();
            MeasurementValue value = new MeasurementValue(Name, "", Unit, AltitudeConfig.Instance.SortOrder);

            if (Bricklet != null)
            {
                int altitudeRaw = Bricklet.GetAltitude();
                value.Value = altitudeRaw / 100.0;
                result.Add(value);
            }

            return result;
        }
    }
}