using System;
using Microsoft.Data.Sqlite;

namespace net.derpaul.tf
{
    /// <summary>
    /// Data sink - sending data using SQLite
    /// </summary>
    public class SQLite : IDataSink
    {
        private SqliteConnection DBConnection { get; set; }

        /// <summary>
        /// Get the name of class
        /// </summary>
        public string Name { get { return this.GetType().Name; } }

        /// <summary>
        /// Flags successful initialization
        /// </summary>
        public bool IsInitialized { get; set; } = false;

        /// <summary>
        /// Disconnect from MQTT broker
        /// </summary>
        public void Shutdown()
        {
            DBConnection.Close();
        }

        /// <summary>
        /// Initialize MQTT client and connect to broker
        /// </summary>
        /// <returns>true on success otherwise false</returns>
        public bool Init()
        {
            bool success = false;

            try
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
                DBConnection = new SqliteConnection($"Data Source={SQLiteConfig.Instance.DatabaseFilename}");
                DBConnection.Open();
                if (DBConnection.State == System.Data.ConnectionState.Open)
                {
                    success = true;
                }
                else
                {
                    System.Console.WriteLine($"{nameof(Init)}: Database [{SQLiteConfig.Instance.DatabaseFilename}] in invalid state: [{DBConnection.State}]");
                }
            }
            catch (SqliteException e)
            {
                System.Console.WriteLine($"{nameof(Init)}: Connection Error [{e.Message}]");
            }

            return success;
        }

        /// <summary>
        /// Transform each result in a JSON string and publish string to topic
        /// </summary>
        /// <param name="SensorValue">Sensor value</param>
        public void HandleValue(MeasurementValue SensorValue)
        {
            HandleValueData(SensorValue);
        }

        /// <summary>
        /// Deals with measurement value
        /// </summary>
        /// <param name="sensorValue"></param>
        private void HandleValueData(MeasurementValue sensorValue)
        {
            if (!TableExists(sensorValue))
            {
                TableCreate(sensorValue);
            }

            InsertData(sensorValue);
        }

        /// <summary>
        /// Execute SQL command
        /// </summary>
        /// <param name="command"></param>
        private void ExecuteSQLStatement(SqliteCommand command)
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
        /// Insert data to table
        /// </summary>
        /// <param name="sensorValue"></param>
        private void InsertData(MeasurementValue sensorValue)
        {
            string stm = $"INSERT INTO {sensorValue.Name} (timestamp, value, hash) VALUES (@timestamp, @value, @hash)";
            SqliteCommand command = new SqliteCommand(stm, DBConnection);
            command.Parameters.Add(new SqliteParameter("@timestamp", System.Data.SqlDbType.DateTime) { Value = sensorValue.Timestamp });
            command.Parameters.Add(new SqliteParameter("@value", System.Data.SqlDbType.DateTime) { Value = sensorValue.Value });
            command.Parameters.Add(new SqliteParameter("@hash", System.Data.SqlDbType.DateTime) { Value = sensorValue.ToHash() });
            ExecuteSQLStatement(command);
        }

        /// <summary>
        /// Create table for measurement value
        /// </summary>
        /// <param name="name">Name of table</param>
        private void TableCreate(MeasurementValue sensorValue)
        {
            string stm = $"CREATE TABLE {sensorValue.Name} (timestamp DATETIME, value DOUBLE, hash VARCHAR(50))";
            SqliteCommand command = new SqliteCommand(stm, DBConnection);
            ExecuteSQLStatement(command);

            stm = $"CREATE INDEX ndx_{sensorValue.Name} ON {sensorValue.Name} (timestamp ASC, hash ASC)";
            command = new SqliteCommand(stm, DBConnection);
            ExecuteSQLStatement(command);
        }

        /// <summary>
        /// Check if table exists in SQLite database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool TableExists(MeasurementValue sensorValue)
        {
            bool exists = false;

            try
            {
                string stm = $"SELECT name FROM sqlite_master WHERE type = 'table' AND name = '{sensorValue.Name}'";
                SqliteCommand cmd = new SqliteCommand(stm, DBConnection);
                SqliteDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    exists = true;
                }
                rdr.Close();
                cmd.Dispose();
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"{nameof(TableExists)}: Cannot create index [ndx_{sensorValue.Name}]: [{e.Message}]");
            }

            return exists;
        }
    }
}