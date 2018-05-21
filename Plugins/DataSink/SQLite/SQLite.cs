using Microsoft.Data.Sqlite;
using System;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Data sink - sending data using SQLite
    /// </summary>
    public class SQLite : IDataSink
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
        /// Dataobject for types
        /// </summary>
        private MeasurementTypes TableTypes = null;

        /// <summary>
        /// Dataobject for units
        /// </summary>
        private MeasurementUnits TableUnits = null;

        /// <summary>
        /// Dataobject for values
        /// </summary>
        private MeasurementValues TableValues = null;

        /// <summary>
        /// Disconnect from MQTT broker
        /// </summary>
        public void Shutdown()
        {
            if (TableTypes != null)
            {
                TableTypes.Shutdown();
            }
            if (TableUnits != null)
            {
                TableUnits.Shutdown();
            }
            if (TableValues != null)
            {
                TableValues.Shutdown();
            }
        }

        /// <summary>
        /// Initialize SQLite connection
        /// </summary>
        /// <returns>true on success otherwise false</returns>
        public bool Init()
        {
            bool success = false;

            try
            {
                SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
                var DBConnection = new SqliteConnection($"Data Source={SQLiteConfig.Instance.DatabaseFilename}");
                DBConnection.Open();
                success = DBConnection.State == System.Data.ConnectionState.Open;

                if (!success)
                {
                    System.Console.WriteLine($"{nameof(Init)}: Database [{SQLiteConfig.Instance.DatabaseFilename}] in invalid state: [{DBConnection.State}]");
                }
                else
                {
                    TableTypes = new MeasurementTypes(DBConnection);
                    TableUnits = new MeasurementUnits(DBConnection);
                    TableValues = new MeasurementValues(DBConnection);

                    if (!TableTypes.TableExists())
                    {
                        TableTypes.TableCreate();
                    }

                    if (!TableUnits.TableExists())
                    {
                        TableUnits.TableCreate();
                    }

                    if (!TableValues.TableExists())
                    {
                        TableValues.TableCreate();
                    }

                    success = TableTypes.TableExists() && TableUnits.TableExists() && TableValues.TableExists();
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
            var mtId = TableTypes.FindID(SensorValue);
            if (mtId == -1)
            {
                TableTypes.InsertValue(SensorValue);
                mtId = TableTypes.FindID(SensorValue);
            }

            var muId = TableUnits.FindID(SensorValue);
            if (muId == -1)
            {
                TableUnits.InsertValue(SensorValue);
                muId = TableUnits.FindID(SensorValue);
            }

            TableValues.InsertValue(SensorValue, mtId, muId);
        }
    }
}