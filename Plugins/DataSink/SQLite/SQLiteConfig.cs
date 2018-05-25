namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Configuration settings of SQLite plugin
    /// </summary>
    public class SQLiteConfig : ConfigLoader<SQLiteConfig>, IConfigObject
    {
        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            DatabaseFilename = "weatherdata.sqlite";
        }

        /// <summary>
        /// Name of the SQLite database file
        /// </summary>
        public string DatabaseFilename { get; set; }
    }
}