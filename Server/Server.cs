using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace net.derpaul.tf
{
    /// <summary>
    /// Recieve data via MQTT and push them to data sink plugins
    /// </summary>
    public class Server
    {
        /// <summary>
        /// The plugin handler
        /// </summary>
        private PluginHandler pluginHandler { get; set; }

        /// <summary>
        /// The MQTT client
        /// </summary>
        private MqttClient MqttClient { get; set; }

        /// <summary>
        /// Main entry point
        /// </summary>
        private static void Main()
        {
            var Server = new Server();
            Server.Run();
        }

        /// <summary>
        /// Main actions
        /// </summary>
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

            pluginHandler.Shutdown();
            MqttClient.Disconnect();

            Environment.Exit(0);
        }

        /// <summary>
        /// Action performed when MQTT message is recieved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string stringJson = Encoding.UTF8.GetString(e.Message);
            MeasurementValue measurementValue = JsonConvert.DeserializeObject<MeasurementValue>(stringJson);
            MqttClient.Publish(ServerConfig.Instance.MQTTTopicHandshake, Encoding.ASCII.GetBytes(measurementValue.ToHash()));
            pluginHandler.HandleValue(measurementValue);
        }
    }
}