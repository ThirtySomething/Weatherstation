using MQTTnet.Client;
using System;
using System.Collections.Generic;

namespace net.derpaul.tf
{
    public class MQTT : IDataSink
    {
        private IMqttClient MqttClient;

        private string ClientId { get; } = System.Guid.NewGuid().ToString();

        /// <summary>
        /// Flags successful initialization
        /// </summary>
        public bool IsInitialized { get; private set; } = false;

        public bool Init()
        {
            bool success = false;

            try
            {
                var factory = new MQTTnet.MqttFactory();

                MqttClient = factory.CreateMqttClient();

                var options = new MqttClientOptionsBuilder()
                    .WithClientId(ClientId)
                    .WithTcpServer(MQTTConfig.Instance.BrokerIP, MQTTConfig.Instance.BrokerPort)
                    .WithCleanSession()
                    .Build();

                MqttClient.ConnectAsync(options);

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

        public void HandleValues(List<MeasurementValue> SensorValues)
        {
        }
    }
}