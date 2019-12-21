using System.Linq;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Write collection of Tinkerforge Sensor plugin values to a database supported by the .net Entity Framework Core
    /// </summary>
    public class Database : IDataSink
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
        /// Reference to the database model
        /// </summary>
        private DBMeasurementModel DBInstance;

        /// <summary>
        /// Initialize DB connection
        /// </summary>
        /// <returns>signal success with true</returns>
        public bool Init()
        {
            bool success = false;

            DBInstance = new DBMeasurementModel();
            success = DBInstance.Database.EnsureCreated();

            return success;
        }

        /// <summary>
        /// Write sensor values to database
        /// </summary>
        /// <param name="SensorValue">Tinkerforge Sensor plugin value</param>
        public void HandleValue(MeasurementValue SensorValue)
        {
            var MType = DetermineMeasurementType(SensorValue);
            var MUnit = DetermineMeasurementUnit(SensorValue);
            WriteSensorData(SensorValue, MType, MUnit);
        }

        /// <summary>
        /// Write the sensor value to the database
        /// </summary>
        /// <param name="SensorValue">Tinkerforge Sensor plugin value</param>
        /// <param name="MType">Used measurement type - the sensor name</param>
        /// <param name="MUnit">used measurement unit</param>
        private void WriteSensorData(MeasurementValue SensorValue, DBMeasurementType MType, DBMeasurementUnit MUnit)
        {
            var MValue = new DBMeasurementValue
            {
                MeasurementType = MType,
                MeasurementUnit = MUnit,
                RecordTime = SensorValue.Timestamp,
                Value = SensorValue.Value
            };
            DBInstance.Add(MValue);
            DBInstance.SaveChanges();
        }

        /// <summary>
        /// Close database connection
        /// </summary>
        public void Shutdown()
        {
            DBInstance.Dispose();
        }

        /// <summary>
        /// Determine measurement type (sensor name) object, create if not exists
        /// </summary>
        /// <param name="SensorValue">Tinkerforge Sensor plugin value</param>
        /// <returns>Referenced measurement type object</returns>
        public DBMeasurementType DetermineMeasurementType(MeasurementValue SensorValue)
        {
            var MType = DBInstance.DBMeasurementTypes.Where(a => a.Name == SensorValue.Name).FirstOrDefault();
            if (MType == null)
            {
                MType = new DBMeasurementType{Name = SensorValue.Name};
                DBInstance.Add(MType);
                DBInstance.SaveChanges();
            }

            return MType;
        }

        /// <summary>
        /// Determine measurement unit object, create if not exists
        /// </summary>
        /// <param name="SensorValue">Tinkerforge Sensor plugin value</param>
        /// <returns>Referenced measurement unit object</returns>
        public DBMeasurementUnit DetermineMeasurementUnit(MeasurementValue SensorValue)
        {
            var MUnit = DBInstance.DBMeasurementUnits.Where(a => a.Name == SensorValue.Unit).FirstOrDefault();
            if (MUnit == null)
            {
                MUnit = new DBMeasurementUnit{Name = SensorValue.Unit};
                DBInstance.Add(MUnit);
                DBInstance.SaveChanges();
            }

            return MUnit;
        }
    }
}
