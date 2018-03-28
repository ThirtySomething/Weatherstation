namespace net.derpaul.tf
{
    /// <summary>
    /// Configuration settings for MQTT
    /// </summary>
    public class SQLiteConfig : ConfigLoader<SQLiteConfig>, IConfigSaver
    {
        /// <summary>
        /// Name of the SQLite database file
        /// </summary>
        public string DatabaseFilename = "weatherdata.sqlite";
    }
}