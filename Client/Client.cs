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
            var pluginHandler = new PluginHandler(pluginPath, ClientConfig.Instance.BrickDaemonIP, ClientConfig.Instance.BrickDaemonPort);

            if (!pluginHandler.Init())
            {
                return;
            }

            for (;;)
            {
                if (!System.Console.KeyAvailable)
                {
                    pluginHandler.HandleValues(pluginHandler.ValuesRead());
                    System.Threading.Thread.Sleep(ClientConfig.Instance.Delay);
                }
                else if (System.Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    break;
                }
            }

            pluginHandler.Shutdown();

            Environment.Exit(0);
        }
    }
}