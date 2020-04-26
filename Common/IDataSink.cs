namespace net.derpaul.tf
{
    /// <summary>
    /// Interface to deal with a collection of Tinkerforge Sensor plugin values
    /// </summary>
    public interface IDataSink : IPlugin
    {
        /// <summary>
        /// Get the name/kind of a sensor
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Initialize plugin with loaded config
        /// </summary>
        /// <returns>signal success with true</returns>
        bool Init();

        /// <summary>
        /// Perform action on a measurement value
        /// </summary>
        /// <param name="SensorValue">A measuremet value</param>
        void HandleValue(MeasurementValue SensorValue);
    }
}