using Microsoft.Data.Sqlite;
using System;

namespace net.derpaul.tf.plugin
{
    public abstract class SQLiteBase
    {
        /// <summary>
        /// Database connection to SQLite
        /// </summary>
        public SqliteConnection DBConnection { get; set; }

        /// <summary>
        /// Disconnect from MQTT broker
        /// </summary>
        public void Shutdown()
        {
            if (DBConnection.State == System.Data.ConnectionState.Open)
            {
                DBConnection.Close();
            }
        }

        /// <summary>
        /// Execute SQL command
        /// </summary>
        /// <param name="command"></param>
        public void ExecuteSQLStatement(SqliteCommand command)
        {
            try
            {
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"{nameof(ExecuteSQLStatement)}: Cannot execute command [{command.ToString()}]: [{e.Message}]");
            }
        }

        /// <summary>
        /// Create table, indices and maybe foreign key constraints
        /// </summary>
        public abstract void TableCreate();

        /// <summary>
        /// Check if table exists
        /// </summary>
        /// <returns>true on existing table, otherwise false</returns>
        public abstract bool TableExists();

        /// <summary>
        /// Find ID for given value
        /// </summary>
        /// <param name="sensorValue"></param>
        /// <returns>ID if found, otherwise -1</returns>
        public abstract int FindID(MeasurementValue sensorValue);

        /// <summary>
        /// Insert new value into table
        /// </summary>
        /// <param name="sensorValue"></param>
        public abstract void InsertValue(MeasurementValue sensorValue, int mtId = 0, int muId = 0);

        /// <summary>
        /// Check if table exists in SQLite database
        /// </summary>
        /// <param name="DBConnection"></param>
        /// <param name="TableName"></param>
        /// <returns>true on existing table, otherwise false</returns>
        internal bool TableExists(string TableName)
        {
            bool exists = false;

            try
            {
                string statement = $"SELECT name FROM sqlite_master WHERE type = 'table' AND name = @tablename";
                SqliteCommand command = new SqliteCommand(statement, DBConnection);
                command.Parameters.Add(new SqliteParameter("@tablename", System.Data.SqlDbType.Text) { Value = TableName });
                SqliteDataReader reader = command.ExecuteReader();

                exists = reader.Read();

                reader.Close();
                command.Dispose();
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"{nameof(TableExists)}: Failure check existence of table [{TableName}]: [{e.Message}]");
            }

            return exists;
        }
    }
}
