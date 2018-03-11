using System;
using Tinkerforge;

namespace net.derpaul.tf
{
    /// <summary>
    /// Abstract base class for all sensors implementing sensor's interface
    /// </summary>
    public abstract class SensorBase : ISensor
    {
        /// <summary>
        /// Measurement unit of sensor
        /// </summary>
        public abstract string Unit { get; }

        /// <summary>
        /// The TF sensor type
        /// </summary>
        public abstract int SensorType { get; }

        /// <summary>
        /// Get the name of the sensor implementation
        /// </summary>
        public string Name
        {
            get
            {
                return GetType().Name;
            }
        }

        /// <summary>
        /// Initialize internal TF bricklet
        /// </summary>
        /// <param name="connection">Connection to master brick</param>
        /// <param name="UID">Sensor ID</param>
        public abstract void Init(IPConnection connection, string UID);

        /// <summary>
        /// Read the value of the sensor, will catch TF exceptions
        /// </summary>
        /// <returns>Sensor value or 0.0</returns>
        public MeasurementValue ValueGet()
        {
            var value = new MeasurementValue();

            try
            {
                value = ValueGetRaw();
            }
            catch (TinkerforgeException e)
            {
                System.Console.WriteLine($"Sensor [{Name}], Error [{e.Message}] ");
            }

            return value;
        }

        /// <summary>
        /// Read the value of the sensor without paying attention to exceptions
        /// </summary>
        /// <returns>Sensor name and value or 0.0</returns>
        protected abstract MeasurementValue ValueGetRaw();
    }
}