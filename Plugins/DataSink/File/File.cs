using System.IO;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Write collection of Tinkerforge Sensor plugin values to a file
    /// </summary>
    public class File : DataSinkBase
    {
        /// <summary>
        /// Stream to write data to
        /// </summary>
        private StreamWriter Datafile { get; set; }

        /// <summary>
        /// Close the file
        /// </summary>
        public override void Shutdown()
        {
            Datafile.Flush();
            Datafile.Close();
        }

        /// <summary>
        /// Write sensor values to file
        /// </summary>
        /// <param name="SensorValue">Tinkerforge Sensor plugin value</param>
        public override void HandleValue(MeasurementValue SensorValue)
        {
            lock (WriteLock)
            {
                Datafile.WriteLine(SensorValue.ToJSON());
            }
        }

        /// <summary>
        /// Open file for appending
        /// </summary>
        /// <returns>signal success with true</returns>
        public override bool Init()
        {
            Datafile = new StreamWriter(FileConfig.Instance.DataFilename, FileConfig.Instance.AppendToFile);

            FileConfig.Instance.ShowConfig();

            bool success = System.IO.File.Exists(FileConfig.Instance.DataFilename);
            return success;
        }
    }
}