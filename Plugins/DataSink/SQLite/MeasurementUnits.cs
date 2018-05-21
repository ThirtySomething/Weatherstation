using Microsoft.Data.Sqlite;
using System;

namespace net.derpaul.tf.plugin
{
    internal class MeasurementUnits : SQLiteBase
    {
        /// <summary>
        /// Tablename to be checked in table exist
        /// </summary>
        internal const string tablename = "measurementunits";

        /// <summary>
        /// Constructor will get active DB connection
        /// </summary>
        /// <param name="dbConnection"></param>
        public MeasurementUnits(SqliteConnection dbConnection)
        {
            DBConnection = dbConnection;
        }

        /// <summary>
        /// Create table and indices
        /// </summary>
        public override void TableCreate()
        {
            string statement = $"CREATE TABLE measurementunits (mu_id INTEGER PRIMARY KEY AUTOINCREMENT, mu_name VARCHAR(50) NOT NULL)";
            SqliteCommand command = new SqliteCommand(statement, DBConnection);
            ExecuteSQLStatement(command);

            statement = $"CREATE INDEX ndx_measurementunits ON measurementunits (mu_name ASC)";
            command = new SqliteCommand(statement, DBConnection);
            ExecuteSQLStatement(command);
        }

        /// <summary>
        /// Check if table exists
        /// </summary>
        /// <returns></returns>
        public override bool TableExists()
        {
            return TableExists(tablename);
        }

        /// <summary>
        /// Find measurement unit in table
        /// </summary>
        /// <param name="sensorValue"></param>
        /// <returns></returns>
        public override int FindID(MeasurementValue sensorValue)
        {
            int unitId = -1;

            try
            {
                string statement = $"SELECT mu_id FROM measurementunits WHERE mu_name = @name";
                SqliteCommand command = new SqliteCommand(statement, DBConnection);
                command.Parameters.Add(new SqliteParameter("@name", System.Data.SqlDbType.Text) { Value = sensorValue.Unit });
                SqliteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    unitId = Convert.ToInt32(reader[0]);
                }
                reader.Close();
                command.Dispose();
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"{nameof(FindID)}: Cannot find id for [{sensorValue.Unit}]: [{e.Message}]");
            }

            return unitId;
        }

        /// <summary>
        /// Write measurement unit to table
        /// </summary>
        /// <param name="sensorValue"></param>
        public override void InsertValue(MeasurementValue sensorValue, int mtId = 0, int muId = 0)
        {
            string statement = $"INSERT INTO measurementunits (mu_name) VALUES (@mu_name)";
            SqliteCommand command = new SqliteCommand(statement, DBConnection);
            command.Parameters.Add(new SqliteParameter("@mu_name", System.Data.SqlDbType.Text) { Value = sensorValue.Unit });
            ExecuteSQLStatement(command);
        }
    }
}
