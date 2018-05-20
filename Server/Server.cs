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
            Environment.Exit(0);
        }

        /// <summary>
        /// Main actions
        /// </summary>
        private void Run()
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ServerConfig.Instance.PluginPath);
            pluginHandler = new PluginHandler(pluginPath);

            if (!pluginHandler.Init())
            {
                return;
            }

            bool connected = false;
            try
            {
                MqttClient = new MqttClient(ServerConfig.Instance.BrokerIP);
                MqttClient.MqttMsgPublishReceived += MqttDataRecieved;
                MqttClient.Connect(ServerConfig.Instance.ClientID);
                MqttClient.Subscribe(new string[] { ServerConfig.Instance.TopicData }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                connected = true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"{nameof(Run)}: Cannot connect to broker [{ServerConfig.Instance.BrokerIP}] => [{e.Message}]");
            }

            if (connected)
            {
                for (; ; )
                {
                    if (!System.Console.KeyAvailable)
                    {
                        TFUtils.WaitNMilliseconds(ServerConfig.Instance.Delay);
                    }
                    else if (System.Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
            }

            pluginHandler.Shutdown();

            if (connected)
            {
                MqttClient.Disconnect();
            }
        }

        /// <summary>
        /// Action performed when MQTT message is recieved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttDataRecieved(object sender, MqttMsgPublishEventArgs e)
        {
            string stringJson = Encoding.UTF8.GetString(e.Message);
            try
            {
                MeasurementValue measurementValue = JsonConvert.DeserializeObject<MeasurementValue>(stringJson);
                MqttClient.Publish(ServerConfig.Instance.TopicAcknowledge, Encoding.ASCII.GetBytes(measurementValue.ToHash()));
                pluginHandler.HandleValue(measurementValue);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"{nameof(MqttDataRecieved)}: Invalid message [{stringJson}] recieved.");
            }
        }
    }
}