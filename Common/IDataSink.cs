namespace net.derpaul.tf
{
    /// <summary>
    /// Interface to deal with a collection of Tinkerforge Sensor plugin values
    /// </summary>
    public interface IDataSink
    {
        /// <summary>
        /// Get the name/kind of a sensor
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Flags successful initialization
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Initialize plugin with loaded config
        /// </summary>
        /// <returns></returns>
        bool Init();

        /// <summary>
        /// Perform action on a measurement value
        /// </summary>
        /// <param name="SensorValue">A measuremet value</param>
        void HandleValue(MeasurementValue SensorValue);

        /// <summary>
        /// Enable plugin to shutdown some resources
        /// </summary>
        void Shutdown();
    }
}