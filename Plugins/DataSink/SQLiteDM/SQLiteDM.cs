using System;
using programmersdigest.DataMapper.Attributes;
using programmersdigest.DataMapper.Migration;
using programmersdigest.DataMapper;
using Microsoft.Data.Sqlite;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Data sink - sending data using SQLiteDM
    /// </summary>
    public class SQLiteDM : IDataSink
    {
        /// <summary>
        /// Get the name of class
        /// </summary>
        public string Name { get { return this.GetType().Name; } }

        /// <summary>
        /// Flags successful initialization
        /// </summary>
        public bool IsInitialized { get; set; } = false;

        /// <summary>
        /// Access to the measurement types
        /// </summary>
        private MeasurementTypesDM TableTypes = null;

        /// <summary>
        /// Disconnect from MQTT broker
        /// </summary>
        public void Shutdown()
        {
        }

        /// <summary>
        /// Initialize SQLiteDM connection
        /// </summary>
        /// <returns>true on success otherwise false</returns>
        public bool Init()
        {
            bool success = false;

            try
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
                var DBConnection = new SqliteConnection($"Data Source={SQLiteDMConfig.Instance.DatabaseFilename}");

                var database = new Database(() => DBConnection,
                                            (cmd) => cmd.CommandText += " SELECT Last_Insert_Rowid();");

                if (TableTypes == null)
                {
                    TableTypes = new MeasurementTypesDM(database);
                    TableTypes.Init();
                }

            }
            catch (Exception e)
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
            var mtId = TableTypes.FindID(SensorValue);
        }
    }
}
