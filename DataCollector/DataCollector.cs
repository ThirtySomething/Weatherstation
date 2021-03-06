using System;
using System.IO;

namespace net.derpaul.tf
{
    /// <summary>
    /// Main program for collecting data
    /// </summary>
    internal class DataCollector
    {
        /// <summary>
        /// Main entry point
        /// </summary>
        private static void Main()
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataCollectorConfig.Instance.PluginPath);
            var pluginHandler = new PluginHandler(pluginPath, DataCollectorConfig.Instance.BrickDaemonIP, DataCollectorConfig.Instance.BrickDaemonPort);

            DataCollectorConfig.Instance.ShowConfig();

            if (!pluginHandler.Init())
            {
                return;
            }

            pluginHandler.StartMeasurements();

            while (true)
            {
                if ((System.Console.KeyAvailable) &&
                    (System.Console.ReadKey(true).Key == ConsoleKey.Escape))
                {
                    pluginHandler.StopMeasurements();
                    break;
                }
            }

            pluginHandler.Shutdown();

            Environment.Exit(0);
        }
    }
}