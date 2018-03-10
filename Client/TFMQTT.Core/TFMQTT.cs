using System;
using System.Collections.Generic;

namespace net.derpaul.tf
{
    public class TFMQTT : ITFDataSink
    {
        public void HandleValues(ICollection<Tuple<string, double>> SensorValues)
        {
        }

        public bool Init()
        {
            return true;
        }
    }
}