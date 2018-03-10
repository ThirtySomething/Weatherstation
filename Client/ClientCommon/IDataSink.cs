using System;
using System.Collections.Generic;

namespace net.derpaul.tf
{
    /// <summary>
    /// Interface to deal with a collection of Tinkerforge Sensor plugin values
    /// </summary>
    public interface IDataSink
    {
        /// <summary>
        /// Initialize plugin with loaded config
        /// </summary>
        /// <returns></returns>
        bool Init();

        /// <summary>
        /// Perform action on measurement values
        /// </summary>
        /// <param name="SensorValues">Collection of collected values</param>
        void HandleValues(ICollection<Tuple<string, double, string>> SensorValues);
    }
}