using System;
using System.IO;

namespace net.derpaul.tf
{
    /// <summary>
    /// Main program to handle data of TF weather station
    /// </summary>
    internal class Device
    {
        /// <summary>
        /// Main entry point
        /// </summary>
        private static void Main()
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DeviceConfig.Instance.PluginPath);
            var pluginHandler = new PluginHandler(pluginPath, DeviceConfig.Instance.BrickDaemonIP, DeviceConfig.Instance.BrickDaemonPort);

            DeviceConfig.Instance.ShowConfig();

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