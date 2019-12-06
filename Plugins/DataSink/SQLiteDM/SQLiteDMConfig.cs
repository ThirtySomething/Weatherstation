namespace net.derpaul.tf
{
    /// <summary>
    /// Configuration settings of SQLiteDM plugin
    /// </summary>
    public class SQLiteDMConfig : ConfigLoader<SQLiteDMConfig>, IConfigObject
    {
        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            DatabaseFilename = "weatherdatapm.sqlite";
        }

        /// <summary>
        /// Name of the SQLite database file
        /// </summary>
        public string DatabaseFilename { get; set; }
    }
}