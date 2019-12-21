using System;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Entity for measurement values
    /// </summary>
    public class DBMeasurementValue
    {
        /// <summary>
        /// ID as primary key
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The measurement type of value
        /// </summary>
        public DBMeasurementType MeasurementType { get; set; }

        /// <summary>
        /// The measurement value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The measurement unit of value
        /// </summary>
        public DBMeasurementUnit MeasurementUnit { get; set; }

        /// <summary>
        /// The timestamp of recording
        /// </summary>
        public DateTime RecordTime { get; set; }
    }
}
