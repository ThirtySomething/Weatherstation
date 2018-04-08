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
            if (!TablesExists())
            {
                TablesCreates();
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
            var mtid = FindTypeID(sensorValue);
            var muid = FindUnitID(sensorValue);

            if (mtid == -1)
            {
                InsertType(sensorValue);
                mtid = FindTypeID(sensorValue);
            }
            if (muid == -1)
            {
                InsertUnit(sensorValue);
                muid = FindUnitID(sensorValue);
            }

            string statement = $"INSERT INTO measurementvalues (mv_mt_id, mv_value, mv_mu_id, mv_timestamp) VALUES (@mv_mt_id, @mv_value, @mv_mu_id, @mv_timestamp)";
            SqliteCommand command = new SqliteCommand(statement, DBConnection);
            command.Parameters.Add(new SqliteParameter("@mv_mt_id", System.Data.SqlDbType.Int) { Value = mtid });
            command.Parameters.Add(new SqliteParameter("@mv_value", System.Data.SqlDbType.Decimal) { Value = sensorValue.Value });
            command.Parameters.Add(new SqliteParameter("@mv_mu_id", System.Data.SqlDbType.Int) { Value = muid });
            command.Parameters.Add(new SqliteParameter("@mv_timestamp", System.Data.SqlDbType.DateTime) { Value = sensorValue.Timestamp });
            ExecuteSQLStatement(command);
        }

        /// <summary>
        /// Write measurement type to table
        /// </summary>
        /// <param name="sensorValue"></param>
        private void InsertType(MeasurementValue sensorValue)
        {
            string statement = $"INSERT INTO measurementtypes (mt_name) VALUES (@mt_name)";
            SqliteCommand command = new SqliteCommand(statement, DBConnection);
            command.Parameters.Add(new SqliteParameter("@mt_name", System.Data.SqlDbType.Text) { Value = sensorValue.Name });
            ExecuteSQLStatement(command);
        }

        /// <summary>
        /// Write measurement unit to table
        /// </summary>
        /// <param name="sensorValue"></param>
        private void InsertUnit(MeasurementValue sensorValue)
        {
            string statement = $"INSERT INTO measurementunits (mu_name) VALUES (@mu_name)";
            SqliteCommand command = new SqliteCommand(statement, DBConnection);
            command.Parameters.Add(new SqliteParameter("@mu_name", System.Data.SqlDbType.Text) { Value = sensorValue.Unit });
            ExecuteSQLStatement(command);
        }

        /// <summary>
        /// Find measurement type in table
        /// </summary>
        /// <param name="sensorValue"></param>
        /// <returns></returns>
        private int FindTypeID(MeasurementValue sensorValue)
        {
            int typeid = -1;

            try
            {
                string statement = $"SELECT mt_id FROM measurementtypes WHERE mt_name = @name";
                SqliteCommand command = new SqliteCommand(statement, DBConnection);
                command.Parameters.Add(new SqliteParameter("@name", System.Data.SqlDbType.Text) { Value = sensorValue.Name });
                SqliteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    typeid = Convert.ToInt32(reader[0]);
                }
                reader.Close();
                command.Dispose();
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"{nameof(FindTypeID)}: Cannot find id for [{sensorValue.Name}]: [{e.Message}]");
            }

            return typeid;
        }

        /// <summary>
        /// Find measurement unit in table
        /// </summary>
        /// <param name="sensorValue"></param>
        /// <returns></returns>
        private int FindUnitID(MeasurementValue sensorValue)
        {
            int unitid = -1;

            try
            {
                string statement = $"SELECT mu_id FROM measurementunits WHERE mu_name = @name";
                SqliteCommand command = new SqliteCommand(statement, DBConnection);
                command.Parameters.Add(new SqliteParameter("@name", System.Data.SqlDbType.Text) { Value = sensorValue.Unit });
                SqliteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    unitid = Convert.ToInt32(reader[0]);
                }
                reader.Close();
                command.Dispose();
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"{nameof(FindUnitID)}: Cannot find id for [{sensorValue.Unit}]: [{e.Message}]");
            }

            return unitid;
        }

        /// <summary>
        /// Create tables for measurements
        /// </summary>
        private void TablesCreates()
        {
            TableCreateMeasurementTypes();
            TableCreateMeasurementUnits();
            TableCreateMeasurementValues();
        }

        /// <summary>
        /// Create table for measurement types
        /// </summary>
        private void TableCreateMeasurementTypes()
        {
            string statement = $"CREATE TABLE measurementtypes (mt_id INTEGER PRIMARY KEY AUTOINCREMENT, mt_name VARCHAR(50) NOT NULL)";
            SqliteCommand command = new SqliteCommand(statement, DBConnection);
            ExecuteSQLStatement(command);

            statement = $"CREATE INDEX ndx_measurementtypes ON measurementtypes (mt_name ASC)";
            command = new SqliteCommand(statement, DBConnection);
            ExecuteSQLStatement(command);
        }

        /// <summary>
        /// Create table for measurement units
        /// </summary>
        private void TableCreateMeasurementUnits()
        {
            string statement = $"CREATE TABLE measurementunits (mu_id INTEGER PRIMARY KEY AUTOINCREMENT, mu_name VARCHAR(50) NOT NULL)";
            SqliteCommand command = new SqliteCommand(statement, DBConnection);
            ExecuteSQLStatement(command);

            statement = $"CREATE INDEX ndx_measurementunits ON measurementunits (mu_name ASC)";
            command = new SqliteCommand(statement, DBConnection);
            ExecuteSQLStatement(command);
        }

        /// <summary>
        /// Create table for measurement values
        /// </summary>
        private void TableCreateMeasurementValues()
        {
            string statement = $"CREATE TABLE measurementvalues (mv_id INTEGER PRIMARY KEY AUTOINCREMENT, mv_mt_id INTEGER REFERENCES measurementtypes(mt_id), mv_value DOUBLE, mv_mu_id INTEGER REFERENCES measurementunits(mu_id), mv_timestamp DATETIME)";
            SqliteCommand command = new SqliteCommand(statement, DBConnection);
            ExecuteSQLStatement(command);

            statement = $"CREATE INDEX ndx_mv_type ON measurementvalues (mv_mt_id ASC, mv_timestamp)";
            command = new SqliteCommand(statement, DBConnection);
            ExecuteSQLStatement(command);

            statement = $"CREATE INDEX ndx_mv_unit ON measurementvalues (mv_mu_id ASC, mv_timestamp)";
            command = new SqliteCommand(statement, DBConnection);
            ExecuteSQLStatement(command);
        }

        /// <summary>
        /// Check if table exists in SQLite database
        /// </summary>
        /// <returns></returns>
        private bool TablesExists()
        {
            bool exists = false;

            try
            {
                string statement = $"SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'measurementvalues'";
                SqliteCommand command = new SqliteCommand(statement, DBConnection);
                SqliteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    exists = true;
                }
                reader.Close();
                command.Dispose();
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"{nameof(TablesExists)}: Failure check existence of table [measurementvalues]: [{e.Message}]");
            }

            return exists;
        }
    }
}