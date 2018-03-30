namespace net.derpaul.tf
{
    /// <summary>
    /// Write collection of Tinkerforge Sensor plugin values to console
    /// </summary>
    public class FileConfig : ConfigLoader<FileConfig>, IConfigSaver
    {
        /// <summary>
        /// Filename to write data to
        /// </summary>
        public string DataFilename = "weatherdata.dat";

        /// <summary>
        /// Flag for appending to existing file or not
        /// </summary>
        public bool AppendToFile = true;
    }
}