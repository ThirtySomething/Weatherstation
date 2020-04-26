using System.Collections.Generic;
using Tinkerforge;

namespace net.derpaul.tf
{
    /// <summary>
    /// Interface of a Tinkerforge Sensor plugin and its capabilities
    /// </summary>
    public interface IDataSource : IPlugin
    {
        /// <summary>
        /// Measurement unit of sensor
        /// </summary>
        string Unit { get; }

        /// <summary>
        /// The TF sensor type
        /// </summary>
        int SensorType { get; }

        /// <summary>
        /// Get the name/kind of a sensor
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Delay in milli seconds until next measurement value is read
        /// </summary>
        int ReadDelay { get; }

        /// <summary>
        /// Initialize internal TF bricklet
        /// </summary>
        /// <param name="connection">Connection to master brick</param>
        /// <param name="UID">Sensor ID</param>
        void Init(IPConnection connection, string UID);

        /// <summary>
        /// Read values of data sink
        /// </summary>
        /// <returns>List of MeasurementValue</returns>
        List<MeasurementValue> Values();
    }
}