using Tinkerforge;

namespace net.derpaul.tf
{
    /// <summary>
    /// Class to read values from barometer sensor
    /// </summary>
    public abstract class Barometer : SensorBase
    {
        /// <summary>
        /// Internal object of TF bricklet
        /// </summary>
        protected static BrickletBarometer _Bricklet { get; set; }

        /// <summary>
        /// The TF sensor type
        /// </summary>
        public override int SensorType { get; } = BrickletBarometer.DEVICE_IDENTIFIER;

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

            _Bricklet = new BrickletBarometer(UID, connection);
        }

        /// <summary>
        /// Abstract method, should be implemented in subclasses. Shall retrieve sensor's value.
        /// </summary>
        /// <returns>Measurement value of sensor or 0.0</returns>
        protected override abstract MeasurementValue ValueGetRaw();
    }
}