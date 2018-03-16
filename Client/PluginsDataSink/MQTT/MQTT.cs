using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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
        /// Queue with measurement values to publish
        /// </summary>
        private Queue<MeasurementValue> DataQueue { get; set; }

        /// <summary>
        /// Published measurement values waiting for acknowledge
        /// </summary>
        private Dictionary<string, MeasurementValue> AckQueue { get; set; }

        /// <summary>
        /// Disconnect from MQTT broker
        /// </summary>
        public void Shutdown()
        {
            MqttClient.Disconnect();
        }

        /// <summary>
        /// Initialize MQTT client and connect to broker
        /// </summary>
        /// <returns>true on success otherwise false</returns>
        public bool Init()
        {
            bool success = false;

            try
            {
                DataQueue = new Queue<MeasurementValue>();
                AckQueue = new Dictionary<string, MeasurementValue>();

                MqttClient = new MqttClient(MQTTConfig.Instance.BrokerIP);
                MqttClient.Connect(MQTTConfig.Instance.MQTTClientID);

                Thread thread = new Thread(HandlePublishQueue);
                thread.Start();

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
            foreach (var currentMeasurementValue in SensorValues)
            {
                lock (DataQueue)
                {
                    DataQueue.Enqueue(currentMeasurementValue);
                }
            }
        }

        /// <summary>
        /// Work on queue and publish data
        /// </summary>
        private void HandlePublishQueue()
        {
            do
            {
                lock (DataQueue)
                {
                    if (DataQueue.Count > 0)
                    {
                        var currentMeasurementData = DataQueue.Dequeue();
                        Thread thread = new Thread(() => PublishSingleValue(currentMeasurementData));
                        thread.Start();
                    }
                }
            } while (true);
        }

        /// <summary>
        /// Publish single measurement data
        /// </summary>
        /// <param name="dataToPublish"></param>
        private void PublishSingleValue(MeasurementValue dataToPublish)
        {
            var message = dataToPublish.ToJSON();
            MqttClient.Publish(MQTTConfig.Instance.MQTTTopicPublish, Encoding.ASCII.GetBytes(message));

            lock (AckQueue)
            {
                AckQueue.Add(dataToPublish.ToHash(), dataToPublish);
            }
        }
    }
}