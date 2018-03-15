using System.Collections.Generic;

namespace net.derpaul.tf
{
    /// <summary>
    /// Write collection of Tinkerforge Sensor plugin values to console
    /// </summary>
    public class Console : IDataSink
    {
        /// <summary>
        /// Flags successful initialization
        /// </summary>
        public bool IsInitialized { get; private set; } = false;

        /// <summary>
        /// Write sensor values to console
        /// </summary>
        /// <param name="SensorValues">Tinkerforge Sensor plugin values</param>
        public void HandleValues(List<MeasurementValue> SensorValues)
        {
            System.Console.WriteLine("---");
            foreach (var currentValue in SensorValues)
            {
                System.Console.WriteLine($"Sensor [{currentValue.Name}], Value [{currentValue.Value}], Unit [{currentValue.Unit}]");
            }
        }

        /// <summary>
        /// Console does not require an init
        /// </summary>
        /// <returns>signal success with true</returns>
        public bool Init()
        {
            IsInitialized = true;
            return true;
        }
    }
}