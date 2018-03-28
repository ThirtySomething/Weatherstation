namespace net.derpaul.tf
{
    /// <summary>
    /// Write collection of Tinkerforge Sensor plugin values to console
    /// </summary>
    public class Console : IDataSink
    {
        /// <summary>
        /// Get the name of class
        /// </summary>
        public string Name { get { return this.GetType().Name; } }

        /// <summary>
        /// Flags successful initialization
        /// </summary>
        public bool IsInitialized { get; set; } = false;

        /// <summary>
        /// Dummy implementation, console has no need for shutdown
        /// </summary>
        public void Shutdown()
        {
        }

        /// <summary>
        /// Write sensor values to console
        /// </summary>
        /// <param name="SensorValue">Tinkerforge Sensor plugin value</param>
        public void HandleValue(MeasurementValue SensorValue)
        {
            System.Console.WriteLine($"Sensor [{SensorValue.Name}], Value [{SensorValue.Value}], Unit [{SensorValue.Unit}]");
        }

        /// <summary>
        /// Console does not require an init
        /// </summary>
        /// <returns>signal success with true</returns>
        public bool Init()
        {
            return true;
        }
    }
}