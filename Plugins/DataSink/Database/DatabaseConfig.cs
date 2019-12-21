using System.Collections.Generic;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Configuration settings of SQLite plugin
    /// </summary>
    public class DatabaseConfig : ConfigLoader<DatabaseConfig>, IConfigObject
    {
        /// <summary>
        /// Supported database types
        /// </summary>
        public enum SupportedDatabaseTypes
        {
            DBSQLite = 1,
            MariaDB = 2
        }

        /// <summary>
        /// Parameter object for SQLite databases
        /// </summary>
        public class ParamSQLite
        {
            public string Filename { get; set; }
        }

        /// <summary>
        /// Parameter object for MariaDB databases
        /// </summary>
        public class ParamMariaDB
        {
            public string Server { get; set; }
            public string UserId { get; set; }
            public string Password { get; set; }
            public string Database { get; set; }
        }

        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            DatabaseType = SupportedDatabaseTypes.DBSQLite;
            DatabaseParameters = "{Filename: \"weatherdata.db\"}";
            //DatabaseType = SupportedDatabaseTypes.MariaDB;
            //DatabaseParameters = "{Server: \"localhost\", UserId: \"wetter\", Password: \"wetter\", Database: \"wetter\"}";
        }

        /// <summary>
        /// The current selected database type
        /// </summary>
        public SupportedDatabaseTypes DatabaseType { get; set; }

        /// <summary>
        /// The database parameters as JSON string
        /// </summary>
        public string DatabaseParameters { get; set; }
    }
}