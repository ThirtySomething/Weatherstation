using Newtonsoft.Json;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace net.derpaul.tf
{
    public class Server
    {
        private MqttClient MqttClient { get; set; }

        /// <summary>
        /// Main entry point
        /// </summary>
        private static void Main()
        {
            var Server = new Server();
            Server.Run();
        }

        private void Run()
        {
            MqttClient = new MqttClient(MQTTConfig.Instance.BrokerIP);
            MqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
            MqttClient.Connect(MQTTConfig.Instance.MQTTClientIDServer);
            MqttClient.Subscribe(new string[] { MQTTConfig.Instance.MQTTTopicData }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string stringJson = Encoding.UTF8.GetString(e.Message);
            MeasurementValue measurementValue = JsonConvert.DeserializeObject<MeasurementValue>(stringJson);
            MqttClient.Publish(MQTTConfig.Instance.MQTTTopicHandshake, Encoding.ASCII.GetBytes(measurementValue.ToHash()));
        }
    }
}