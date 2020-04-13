using System;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Write collection of Tinkerforge Sensor plugin values to console
    /// </summary>
    public class Console : DataSinkBase
    {
        /// <summary>
        /// Write sensor value to console
        /// </summary>
        /// <param name="SensorValue">Tinkerforge Sensor plugin value</param>
        public override void HandleValue(MeasurementValue SensorValue)
        {
            lock (WriteLock)
            {
                System.Console.WriteLine($"Sensor [{SensorValue.Name}], Value [{SensorValue.Value}], Unit [{SensorValue.Unit}]");
            }
        }
    }
}