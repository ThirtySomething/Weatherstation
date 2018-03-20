namespace net.derpaul.tf
{
    /// <summary>
    /// Write collection of Tinkerforge Sensor plugin values to console
    /// </summary>
    public class FileConfig : ConfigLoader<FileConfig>, ConfigSaver
    {
        /// <summary>
        /// Filename to write data to
        /// </summary>
        public string DataFilename = "clientdata.dat";
    }
}