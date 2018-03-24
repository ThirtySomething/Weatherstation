using System;
using System.IO;

namespace net.derpaul.tf
{
    /// <summary>
    /// Main program to handle data of TF weather station
    /// </summary>
    internal class Client
    {
        /// <summary>
        /// Main entry point
        /// </summary>
        private static void Main()
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ClientConfig.Instance.PluginPath);
            var pluginHandler = new PluginHandler(pluginPath, ClientConfig.Instance.Host, ClientConfig.Instance.Port);

            if (pluginHandler.Init() == false)
            {
                return;
            }

            TFUtils.WaitUntilStart();
            do
            {
                while (!System.Console.KeyAvailable)
                {
                    pluginHandler.HandleValues(pluginHandler.ValuesRead());
                    TFUtils.WaitNMilliseconds(ClientConfig.Instance.Delay);
                }
            } while (System.Console.ReadKey(true).Key != ConsoleKey.Escape);

            pluginHandler.Shutdown();

            Environment.Exit(0);
        }
    }
}