using System.Collections.Generic;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Entity for measurement units
    /// </summary>
    public class MUnit
    {
        /// <summary>
        /// ID as primary key
        /// </summary>
        public ulong ID { get; set; }

        /// <summary>
        /// Name as field for unit name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Used for foreign key setup
        /// </summary>
        public ICollection<MValue> Values { get; set; }
    }
}
