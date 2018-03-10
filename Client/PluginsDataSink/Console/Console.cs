using System;
using System.Collections.Generic;

namespace net.derpaul.tf
{
    /// <summary>
    /// Write collection of Tinkerforge Sensor plugin values to console
    /// </summary>
    public class Console : IDataSink
    {
        /// <summary>
        /// This plugin does not have a configuration
        /// </summary>
        /// <returns>signal success with true</returns>
        public bool ConfigLoad()
        {
            return true;
        }

        /// <summary>
        /// Write sensor values to console
        /// </summary>
        /// <param name="SensorValues">Tinkerforge Sensor plugin values</param>
        public void HandleValues(ICollection<Tuple<string, double, string>> SensorValues)
        {
            foreach (var currentValue in SensorValues)
            {
                System.Console.WriteLine($"Sensor [{currentValue.Item1}], Value [{currentValue.Item2}], Unit [{currentValue.Item3}]");
            }
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