using System;
using System.Collections.Generic;
using uPLibrary.Networking.M2Mqtt;

namespace net.derpaul.tf
{
    public class MQTT : IDataSink
    {
        private MqttClient MqttClient;

        /// <summary>
        /// Flags successful initialization
        /// </summary>
        public bool IsInitialized { get; private set; } = false;

        public bool Init()
        {
            bool success = false;

            try
            {
                MqttClient = new MqttClient(MQTTConfig.Instance.BrokerIP);
                success = true;
                IsInitialized = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: Cannot connect to broker [{MQTTConfig.Instance.BrokerIP}] => [{e.Message}]");
                success = false;
            }

            return success;
        }

        public void HandleValues(ICollection<Result> SensorValues)
        {
        }
    }
}