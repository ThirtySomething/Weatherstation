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

            if (SensorHandler.Connect() == false)
            {
                return;
            }

            SensorHandler.Init();

            ICollection<Tuple<string, double>> pluginValues = SensorHandler.ValuesRead();

            int loop = 10;
            while (loop > 0)
            {
                Console.WriteLine("---");
                foreach (Tuple<string, double> currentValue in pluginValues)
                {
                    Console.WriteLine($"Sensor [{currentValue.Item1}], Value [{currentValue.Item2}]");
                }
                loop--;
                System.Threading.Thread.Sleep(Defines.constDefaultDelay);
                pluginValues = SensorHandler.ValuesRead();
            }
        }
    }
}