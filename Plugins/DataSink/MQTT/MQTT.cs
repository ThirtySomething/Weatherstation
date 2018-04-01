using System;
using System.Collections.Generic;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Data sink - sending data using MQTT
    /// </summary>
    public class MQTT : IDataSink
    {
        /// <summary>
        /// Get the name of class
        /// </summary>
        public string Name { get { return this.GetType().Name; } }

        /// <summary>
        /// Instance of M2Mqtt client
        /// </summary>
        private MqttClient MqttClient;

        /// <summary>
        /// Flags successful initialization
        /// </summary>
        public bool IsInitialized { get; set; } = false;

        /// <summary>
        /// Published measurement values waiting for acknowledge
        /// </summary>
        private Dictionary<string, MeasurementValue> AcknowledgeList { get; set; }

        /// <summary>
        /// Object to lock while modifying acknowledge list
        /// </summary>
        private Object Locker = new Object();

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
                AcknowledgeList = new Dictionary<string, MeasurementValue>();

                MqttClient = new MqttClient(MQTTConfig.Instance.BrokerIP);
                MqttClient.MqttMsgPublishReceived += MqttAcknowledgeRecieved;
                MqttClient.Connect(MQTTConfig.Instance.ClientID);
                MqttClient.Subscribe(new string[] { MQTTConfig.Instance.TopicAcknowledge }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

                success = MqttClient.IsConnected;
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"{nameof(Init)}: Cannot connect to broker [{MQTTConfig.Instance.BrokerIP}] => [{e.Message}]");
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Transform each result in a JSON string and publish string to topic
        /// </summary>
        /// <param name="SensorValue">Sensor value</param>
        public void HandleValue(MeasurementValue SensorValue)
        {
            PublishSingleValue(SensorValue);
        }

        /// <summary>
        /// Publish single measurement data
        /// </summary>
        /// <param name="dataToPublish"></param>
        private void PublishSingleValue(MeasurementValue dataToPublish)
        {
            var dataJSON = dataToPublish.ToJSON();

            lock (Locker)
            {
                AcknowledgeList.Add(dataToPublish.ToHash(), dataToPublish);
            }

            MqttClient.Publish(MQTTConfig.Instance.TopicData, Encoding.ASCII.GetBytes(dataJSON));
        }

        /// <summary>
        /// Handles response of server and removes measurement values from internal acknowledge list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void MqttAcknowledgeRecieved(object sender, MqttMsgPublishEventArgs e)
        {
            string messageHash = Encoding.UTF8.GetString(e.Message);

            lock (Locker)
            {
                if (AcknowledgeList.ContainsKey(messageHash))
                {
                    AcknowledgeList.Remove(messageHash);
                }
            }
        }
    }
}