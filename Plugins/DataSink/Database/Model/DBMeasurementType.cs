using System.Collections.Generic;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Entity for measurement types
    /// </summary>
    public class DBMeasurementType
    {
        /// <summary>
        /// ID as primary key
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name as field for sensor type
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Used for foreign key setup
        /// </summary>
        public ICollection<DBMeasurementValue> MeasurementValues { get; set; }
    }
}
