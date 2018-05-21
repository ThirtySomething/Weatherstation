using Microsoft.Data.Sqlite;

namespace net.derpaul.tf.plugin
{
    internal class MeasurementValues : SQLiteBase
    {
        /// <summary>
        /// Tablename to be checked in table exist
        /// </summary>
        internal const string tablename = "measurementvalues";

        /// <summary>
        /// Constructor will get active DB connection
        /// </summary>
        /// <param name="dbConnection"></param>
        public MeasurementValues(SqliteConnection dbConnection)
        {
            DBConnection = dbConnection;
        }

        /// <summary>
        /// Not required at the moment
        /// </summary>
        /// <param name="sensorValue"></param>
        /// <returns></returns>
        public override int FindID(MeasurementValue sensorValue)
        {
            return -1;
        }

        /// <summary>
        /// Write measurement unit to table
        /// </summary>
        /// <param name="sensorValue"></param>
        public override void InsertValue(MeasurementValue sensorValue, int mtId = 0, int muId = 0)
        {
            string statement = $"INSERT INTO measurementvalues (mv_mt_id, mv_value, mv_mu_id, mv_timestamp) VALUES (@mv_mt_id, @mv_value, @mv_mu_id, @mv_timestamp)";
            SqliteCommand command = new SqliteCommand(statement, DBConnection);
            command.Parameters.Add(new SqliteParameter("@mv_mt_id", System.Data.SqlDbType.Int) { Value = mtId });
            command.Parameters.Add(new SqliteParameter("@mv_value", System.Data.SqlDbType.Decimal) { Value = sensorValue.Value });
            command.Parameters.Add(new SqliteParameter("@mv_mu_id", System.Data.SqlDbType.Int) { Value = muId });
            command.Parameters.Add(new SqliteParameter("@mv_timestamp", System.Data.SqlDbType.DateTime) { Value = sensorValue.Timestamp });
            ExecuteSQLStatement(command);
        }

        /// <summary>
        /// Create table, indices and foreign key constraints
        /// </summary>
        public override void TableCreate()
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
        /// Check if table exists
        /// </summary>
        /// <returns></returns>
        public override bool TableExists()
        {
            return TableExists(tablename);
        }
    }
}
