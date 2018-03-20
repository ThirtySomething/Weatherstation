using System.Collections.Generic;

namespace net.derpaul.tf
{
    /// <summary>
    /// Write collection of Tinkerforge Sensor plugin values to a file
    /// </summary>
    public class File : IDataSink
    {
        /// <summary>
        /// Stream to write data to
        /// </summary>
        private System.IO.StreamWriter Datafile { get; set; }

        /// <summary>
        /// Flags successful initialization
        /// </summary>
        public bool IsInitialized { get; private set; } = false;

        /// <summary>
        /// Close the file
        /// </summary>
        public void Shutdown()
        {
            Datafile.Flush();
            Datafile.Close();
        }

        /// <summary>
        /// Write sensor values to file
        /// </summary>
        /// <param name="SensorValues">Tinkerforge Sensor plugin values</param>
        public void HandleValues(List<MeasurementValue> SensorValues)
        {
            foreach (var currentValue in SensorValues)
            {
                Datafile.WriteLine(currentValue.ToJSON());
            }
        }

        /// <summary>
        /// Console does not require an init
        /// </summary>
        /// <returns>signal success with true</returns>
        public bool Init()
        {
            Datafile = new System.IO.StreamWriter(FileConfig.Instance.DataFilename);

            IsInitialized = true;
            return true;
        }
    }
}