using System.Collections.Generic;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Entity for measurement locations
    /// </summary>
    public class MLocation
    {
        /// <summary>
        /// ID as primary key
        /// </summary>
        public ulong ID { get; set; }

        /// <summary>
        /// Measurement location
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Used for foreign key setup
        /// </summary>
        public ICollection<MValue> Values { get; set; }
    }
}
