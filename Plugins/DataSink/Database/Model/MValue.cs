using System;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Entity for measurement values
    /// </summary>
    public class MValue
    {
        /// <summary>
        /// ID as primary key
        /// </summary>
        public ulong ID { get; set; }

        /// <summary>
        /// The measurement type of value
        /// </summary>
        public MType Type { get; set; }

        /// <summary>
        /// The measurement location of value
        /// </summary>
        public MLocation Location { get; set; }

        /// <summary>
        /// The measurement value
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// The measurement unit of value
        /// </summary>
        public MUnit Unit { get; set; }

        /// <summary>
        /// The timestamp of recording
        /// </summary>
        public DateTime RecordTime { get; set; }
    }
}
