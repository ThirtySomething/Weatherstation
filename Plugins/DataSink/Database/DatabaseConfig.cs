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
            /// <summary>
            /// Filename for the SQLite database file
            /// </summary>
            public string Filename { get; set; }
        }

        /// <summary>
        /// Parameter object for MariaDB databases
        /// </summary>
        public class ParamMariaDB
        {
            /// <summary>
            /// The server name or IP of the database
            /// </summary>
            public string Server { get; set; }

            /// <summary>
            /// The username to connect to
            /// </summary>
            public string UserId { get; set; }

            /// <summary>
            /// The password for the username to connect
            /// </summary>
            public string Password { get; set; }

            /// <summary>
            /// The database schema used to write to
            /// </summary>
            public string Database { get; set; }
        }

        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            DatabaseType = SupportedDatabaseTypes.DBSQLite;
            DatabaseOptions = "{Filename: \"weatherdata.db\"}";
            //DatabaseType = SupportedDatabaseTypes.MariaDB;
            //DatabaseOptions = "{Server: \"localhost\", UserId: \"weatheruser\", Password: \"weatherpassword\", Database: \"weatherdatabase\"}";
        }

        /// <summary>
        /// The current selected database type
        /// </summary>
        public SupportedDatabaseTypes DatabaseType { get; set; }

        /// <summary>
        /// The database parameters as JSON string
        /// </summary>
        public string DatabaseOptions { get; set; }
    }
}