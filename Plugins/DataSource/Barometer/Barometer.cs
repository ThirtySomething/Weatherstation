using System.Collections.Generic;
using Tinkerforge;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Class to read values from barometer sensor
    /// </summary>
    public abstract class Barometer : DataSourceBase
    {
        /// <summary>
        /// Internal object of TF barometer bricklet
        /// </summary>
        protected static BrickletBarometer Bricklet { get; set; }

        /// <summary>
        /// The TF sensor type
        /// </summary>
        public override int SensorType { get; } = BrickletBarometer.DEVICE_IDENTIFIER;

        /// <summary>
        /// Initialize internal TF bricklet
        /// </summary>
        /// <param name="connection">Connection to master brick</param>
        /// <param name="UID">Sensor ID</param>
        /// <returns>true on successful init</returns>
        public override bool Init(IPConnection connection, string UID)
        {
            if (Bricklet == null)
            {
                Bricklet = new BrickletBarometer(UID, connection);
            }
            return true;
        }

        /// <summary>
        /// Abstract method, should be implemented in subclasses. Shall retrieve sensor's value.
        /// </summary>
        /// <returns>Measurement value of sensor or 0.0</returns>
        protected override abstract List<MeasurementValue> RawValues();
    }
}