namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Configuration settings of file plugin to write data to a file
    /// </summary>
    public class FileConfig : ConfigLoader<FileConfig>, IConfigObject
    {
        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            PluginDelay = TFUtils.DefaultDelay;
            DataFilename = "weatherdata.dat";
            AppendToFile = true;
        }

        /// <summary>
        /// Filename to write data to
        /// </summary>
        public string DataFilename { get; set; }

        /// <summary>
        /// Flag for appending to existing file or not
        /// </summary>
        public bool AppendToFile { get; set; }

        /// <summary>
        /// Inidividual delay time of each plugin.
        /// </summary>
        public int PluginDelay { get; set; }
    }
}