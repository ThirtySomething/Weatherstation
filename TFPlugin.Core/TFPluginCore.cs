using System;
using System.Collections.Generic;
using System.IO;

namespace net.derpaul.tf
{
    /// <summary>
    /// Main program to run on M$ Windows, reading data from TF weather station
    /// </summary>
    internal class TFPluginCore
    {
        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line agruments</param>
        private static void Main(string[] args)
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Constants.constPluginPath);
            TFHandler SensorHandler = new TFHandler(pluginPath, TFPluginCoreConfig.Instance.Host, TFPluginCoreConfig.Instance.Port);

            if (SensorHandler.Init() == false)
            {
                return;
            }

            ICollection<Tuple<string, double>> pluginValues = SensorHandler.ValuesRead();

            int loop = 10;
            while (loop > 0)
            {
                Console.WriteLine("---");
                SensorHandler.HandleValues(pluginValues);
                loop--;
                System.Threading.Thread.Sleep(TFPluginCoreConfig.Instance.Delay);
                pluginValues = SensorHandler.ValuesRead();
            }
        }
    }
}