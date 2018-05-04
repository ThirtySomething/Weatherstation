namespace net.derpaul.tf
{
    /// <summary>
    /// Configuration settings of file plugin to write data to a file
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