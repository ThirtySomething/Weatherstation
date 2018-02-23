using System;
using System.Collections.Generic;

namespace net.derpaul.tf
{
    public interface TFDataSink
    {
        /// <summary>
        /// Load plugin configuration
        /// </summary>
        /// <returns></returns>
        bool ConfigLoad();

        /// <summary>
        /// Initialize plugin with loaded config
        /// </summary>
        /// <returns></returns>
        bool Init();

        /// <summary>
        /// Perform action on measurement values
        /// </summary>
        /// <param name="SensorValues">List of collected values</param>
        void HandleValues(List<Tuple<string, double>> SensorValues);
    }
}