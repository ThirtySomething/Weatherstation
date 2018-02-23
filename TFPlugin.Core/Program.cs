using System;
using System.Collections.Generic;
using System.IO;

namespace net.derpaul.tf
{
    /// <summary>
    /// Main program to run on M$ Windows, reading data from TF weather station
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main entry point
        /// </summary>
        /// <param name="args">Command line agruments</param>
        private static void Main(string[] args)
        {
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Defines.constPluginPath);
            TFHandler SensorHandler = new TFHandler(pluginPath, Defines.constDefaultHost, Defines.constDefaultPort);

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
                System.Threading.Thread.Sleep(Defines.constDefaultDelay);
                pluginValues = SensorHandler.ValuesRead();
            }
        }
    }
}