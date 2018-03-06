using System;
using System.Collections.Generic;

namespace net.derpaul.tf
{
    public class TFMQTT : ITFDataSink
    {
        public void HandleValues(ICollection<Tuple<string, double>> SensorValues)
        {
            throw new NotImplementedException();
        }

        public bool Init()
        {
            throw new NotImplementedException();
        }
    }
}