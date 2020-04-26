using Newtonsoft.Json;
using System;

namespace net.derpaul.tf
{
    /// <summary>
    /// Result of one measurement:
    /// - Measurement value
    /// - Unit of measurement value
    /// - PluginName/kind of sensor
    /// - Timestamp of recording
    /// </summary>
    public class MeasurementValue
    {
        /// <summary>
        /// PluginName/kind of sensor
        /// </summary>
        public string PluginName { get; set; }

        /// <summary>
        /// Location of sensor
        /// </summary>
        public string SensorLocation { get; set; }

        /// <summary>
        /// Sort priority
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Timestamp of recording
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Unit of measurement value
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Measurement value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MeasurementValue() : this("", "", "", 1)
        {
        }

        /// <summary>
        /// Constructor to initialize values
        /// </summary>
        /// <param name="pluginName">Name of data source plugin</param>
        /// <param name="sensorLocation">Location of the sensor</param>
        /// <param name="valueUnit">Unit of measurement value</param>
        /// <param name="sortOrder">Sort order for Tinkerforge LCD display</param>
        public MeasurementValue(string pluginName, string sensorLocation, string valueUnit, int sortOrder)
        {
            PluginName = pluginName;
            SensorLocation = sensorLocation;
            Timestamp = DateTime.Now;
            Unit = valueUnit;
            Value = 0.0;
            SortOrder = (sortOrder - 1);
            if (SortOrder < 0)
            {
                SortOrder = 0;
            }
        }

        /// <summary>
        /// Get object as JSON string
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Get object as hash
        /// </summary>
        /// <returns>Hash value of measurement value</returns>
        public string ToHash()
        {
            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] stringJSON = System.Text.Encoding.UTF8.GetBytes(ToJSON());
                byte[] stringHash = sha.ComputeHash(stringJSON);
                return BitConverter.ToString(stringHash).Replace("-", String.Empty);
            }
        }
    }
}