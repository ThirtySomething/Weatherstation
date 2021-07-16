using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Write collection of Tinkerforge Sensor plugin values to a database supported by the .net Entity Framework Core
    /// </summary>
    public class Database : DataSinkBase
    {
        /// <summary>
        /// Reference to the database model
        /// </summary>
        private MModel DBInstance;

        /// <summary>
        /// Initialize DB connection
        /// </summary>
        /// <returns>signal success with true</returns>
        public override bool Init()
        {
            DatabaseConfig.Instance.ShowConfig();
            DBInstance = new MModel(new DbContextOptions < MModel > ());
            DBInstance.Database.EnsureCreated();

            return true;
        }

        /// <summary>
        /// Write sensor values to database
        /// </summary>
        /// <param name="SensorValue">Tinkerforge Sensor plugin value</param>
        public override void HandleValue(MeasurementValue SensorValue)
        {
            lock (WriteLock)
            {
                var MType = DetermineMeasurementType(SensorValue);
                var MUnit = DetermineMeasurementUnit(SensorValue);
                var MLocation = DetermineMeasurementLocation(SensorValue);
                WriteSensorData(SensorValue, MType, MUnit, MLocation);
            }
        }

        /// <summary>
        /// Write the sensor value to the database
        /// </summary>
        /// <param name="SensorValue">Tinkerforge Sensor plugin value</param>
        /// <param name="MType">Used measurement type - the sensor name</param>
        /// <param name="MUnit">used measurement unit</param>
        /// <param name="MLocation">used measurement location</param>
        private void WriteSensorData(MeasurementValue SensorValue, MType MType, MUnit MUnit, MLocation MLocation)
        {
            var MValue = new MValue
            {
                Type = MType,
                Unit = MUnit,
                Location = MLocation,
                RecordTime = SensorValue.Timestamp,
                Value = SensorValue.Value
            };
            DBInstance.Add(MValue);
            DBInstance.SaveChanges();
        }

        /// <summary>
        /// Close database connection
        /// </summary>
        public override void Shutdown()
        {
            DBInstance.Dispose();
        }

        /// <summary>
        /// Determine measurement type (sensor name) object, create if not exists
        /// </summary>
        /// <param name="SensorValue">Tinkerforge Sensor plugin value</param>
        /// <returns>Referenced measurement type object</returns>
        public MType DetermineMeasurementType(MeasurementValue SensorValue)
        {
            var MType = DBInstance.DBMeasurementTypes.Where(a => a.Name == SensorValue.PluginName).FirstOrDefault();
            if (MType == null)
            {
                MType = new MType { Name = SensorValue.PluginName };
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
        public MUnit DetermineMeasurementUnit(MeasurementValue SensorValue)
        {
            var MUnit = DBInstance.DBMeasurementUnits.Where(a => a.Name == SensorValue.Unit).FirstOrDefault();
            if (MUnit == null)
            {
                MUnit = new MUnit { Name = SensorValue.Unit };
                DBInstance.Add(MUnit);
                DBInstance.SaveChanges();
            }

            return MUnit;
        }


        /// <summary>
        /// Determine measurement location object, create if not exists
        /// </summary>
        /// <param name="SensorValue">Tinkerforge Sensor plugin value</param>
        /// <returns>Referenced measurement location object</returns>
        public MLocation DetermineMeasurementLocation(MeasurementValue SensorValue)
        {
            var MLocation = DBInstance.DBMeasurementLocations.Where(a => a.Name == SensorValue.SensorLocation).FirstOrDefault();
            if (MLocation == null)
            {
                MLocation = new MLocation { Name = SensorValue.SensorLocation };
                DBInstance.Add(MLocation);
                DBInstance.SaveChanges();
            }

            return MLocation;
        }
    }
}
