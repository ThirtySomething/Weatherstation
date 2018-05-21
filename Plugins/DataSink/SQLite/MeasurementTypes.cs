using Microsoft.Data.Sqlite;
using System;

namespace net.derpaul.tf.plugin
{
    public class MeasurementTypes : SQLiteBase
    {
        /// <summary>
        /// Tablename to be checked in table exist
        /// </summary>
        internal const string tablename = "measurementtypes";

        /// <summary>
        /// Constructor will get active DB connection
        /// </summary>
        /// <param name="dbConnection"></param>
        public MeasurementTypes(SqliteConnection dbConnection)
        {
            DBConnection = dbConnection;
        }

        /// <summary>
        /// Create table and indices
        /// </summary>
        public override void TableCreate()
        {
            string statement = $"CREATE TABLE measurementtypes (mt_id INTEGER PRIMARY KEY AUTOINCREMENT, mt_name VARCHAR(50) NOT NULL)";
            SqliteCommand command = new SqliteCommand(statement, DBConnection);
            ExecuteSQLStatement(command);

            statement = $"CREATE INDEX ndx_measurementtypes ON measurementtypes (mt_name ASC)";
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
        /// Find measurement type in table
        /// </summary>
        /// <param name="sensorValue"></param>
        /// <returns></returns>
        public override int FindID(MeasurementValue sensorValue)
        {
            int typeId = -1;

            try
            {
                string statement = $"SELECT mt_id FROM measurementtypes WHERE mt_name = @name";
                SqliteCommand command = new SqliteCommand(statement, DBConnection);
                command.Parameters.Add(new SqliteParameter("@name", System.Data.SqlDbType.Text) { Value = sensorValue.Name });
                SqliteDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    typeId = Convert.ToInt32(reader[0]);
                }
                reader.Close();
                command.Dispose();
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"{nameof(FindID)}: Cannot find id for [{sensorValue.Name}]: [{e.Message}]");
            }

            return typeId;
        }

        /// <summary>
        /// Write measurement type to table
        /// </summary>
        /// <param name="sensorValue"></param>
        public override void InsertValue(MeasurementValue sensorValue, int mtId = 0, int muId = 0)
        {
            string statement = $"INSERT INTO measurementtypes (mt_name) VALUES (@mt_name)";
            SqliteCommand command = new SqliteCommand(statement, DBConnection);
            command.Parameters.Add(new SqliteParameter("@mt_name", System.Data.SqlDbType.Text) { Value = sensorValue.Name });
            ExecuteSQLStatement(command);
        }
    }
}
