using System;
using System.Collections.Generic;
using System.Text;
using uPLibrary.Networking.M2Mqtt;

namespace net.derpaul.tf
{
    public class MQTT : IDataSink
    {
        /// <summary>
        /// Instance of M2Mqtt client
        /// </summary>
        private MqttClient MqttClient;

        /// <summary>
        /// Flags successful initialization
        /// </summary>
        public bool IsInitialized { get; private set; } = false;

        /// <summary>
        /// Initialize MQTT client and connect to broker
        /// </summary>
        /// <returns>true on success otherwise false</returns>
        public bool Init()
        {
            bool success = false;

            try
            {
                MqttClient = new MqttClient(MQTTConfig.Instance.BrokerIP);
                MqttClient.Connect(MQTTConfig.Instance.MQTTClientID);
                success = MqttClient.IsConnected;
                IsInitialized = success;
            }
            catch (Exception e)
            {
                Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: Cannot connect to broker [{MQTTConfig.Instance.BrokerIP}] => [{e.Message}]");
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Transform each result in a JSON string and publish string to topic
        /// </summary>
        /// <param name="SensorValues">List of all sensor values</param>
        public void HandleValues(List<MeasurementValue> SensorValues)
        {
            if (!MqttClient.IsConnected)
            {
                Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: Lost connection to broker [{MQTTConfig.Instance.BrokerIP}]");
                return;
            }

            foreach (var currentMeasurementValue in SensorValues)
            {
                var message = currentMeasurementValue.ToJSON();
                MqttClient.Publish(MQTTConfig.Instance.MQTTTopic, Encoding.ASCII.GetBytes(message));
            }
        }
    }
}