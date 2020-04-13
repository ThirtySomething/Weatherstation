namespace net.derpaul.tf
{
    /// <summary>
    /// Object represents a Tinkerforge sensor meta information returned by enumerate event
    /// </summary>
    public class TFSensor
    {
        /// <summary>
        /// UID of sensor
        /// </summary>
        public string UID { get; set; }

        /// <summary>
        /// UID of master brick
        /// </summary>
        public string ConnectedUID { get; set; }

        /// <summary>
        /// Position of sensor in stack
        /// </summary>
        public char Position { get; set; }

        /// <summary>
        /// HArdware version of sensor
        /// </summary>
        public string HardwareVersion { get; set; }

        /// <summary>
        /// Firmware version of sensor
        /// </summary>
        public string FirmwareVersion { get; set; }

        /// <summary>
        /// Device identifier => sensor type
        /// </summary>
        public int DeviceIdentifier { get; set; }

        /// <summary>
        /// Enumeration type of sensor
        /// </summary>
        public short EnumerationType { get; set; }

        /// <summary>
        /// Get sensor information as string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Sensor [{UID}], type [{DeviceIdentifier}], hardware [{HardwareVersion}], firmware [{FirmwareVersion}].";
        }
    }
}