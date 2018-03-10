using System;
using System.Collections.Generic;

namespace net.derpaul.tf
{
    public class MQTT : IDataSink
    {
        public void HandleValues(ICollection<Tuple<string, double, string>> SensorValues)
        {
        }

        public bool Init()
        {
            return true;
        }
    }
}