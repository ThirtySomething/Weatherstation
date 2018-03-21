using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace net.derpaul.tf
{
    public class Server
    {
        private PluginHandler pluginHandler { get; set; }

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
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ServerConfig.Instance.PluginPath);
            pluginHandler = new PluginHandler(pluginPath);

            if (pluginHandler.Init() == false)
            {
                return;
            }

            MqttClient = new MqttClient(ServerConfig.Instance.BrokerIP);
            MqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
            MqttClient.Connect(ServerConfig.Instance.MQTTClientIDServer);
            MqttClient.Subscribe(new string[] { ServerConfig.Instance.MQTTTopicData }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

            do
            {
                while (!System.Console.KeyAvailable)
                {
                    UtilsTF.WaitNMilliseconds(ServerConfig.Instance.Delay);
                }
            } while (System.Console.ReadKey(true).Key != ConsoleKey.Escape);

            Environment.Exit(0);
        }

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string stringJson = Encoding.UTF8.GetString(e.Message);
            MeasurementValue measurementValue = JsonConvert.DeserializeObject<MeasurementValue>(stringJson);
            MqttClient.Publish(ServerConfig.Instance.MQTTTopicHandshake, Encoding.ASCII.GetBytes(measurementValue.ToHash()));
            pluginHandler.HandleValue(measurementValue);
        }
    }
}