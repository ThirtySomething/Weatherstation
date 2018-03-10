using System;

namespace net.derpaul.tf
{
    /// <summary>
    /// Result of one measurement:
    /// - Measurement value
    /// - Unit of measurement value
    /// - Name/kind of sensor
    /// - Timestamp of recording
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Measurement value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Unit of measurement value
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Name/kind of sensor
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Timestamp of recording
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Result() : this("", "")
        {
        }

        /// <summary>
        /// Constructor to initialize values
        /// </summary>
        public Result(string sensorName, string valueUnit)
        {
            Value = 0.0;
            Unit = valueUnit;
            Name = sensorName;
            Timestamp = DateTime.Now;
        }
    }
}