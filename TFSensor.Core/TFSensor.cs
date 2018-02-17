using Tinkerforge;

namespace net.derpaul.tf
{
    /// <summary>
    /// Interface of a Tinkerforge Sensor plugin and its capabilities
    /// </summary>
    public interface ITFSensor
    {
        /// <summary>
        /// The TF sensor type
        /// </summary>
        int SensorType { get; }

        /// <summary>
        /// Get the name/kind of a sensor
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Initialize internal TF bricklet
        /// </summary>
        /// <param name="connection">Connection to master brick</param>
        /// <param name="UID">Sensor ID</param>
        void Init(IPConnection connection, string UID);

        /// <summary>
        /// Read the value of the sensor
        /// </summary>
        /// <returns>Sensor value</returns>
        double ValueGet();
    }
}