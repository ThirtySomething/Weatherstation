using System;

namespace net.derpaul.tf
{
    /// <summary>
    /// Abstract base class for all data sinks implementing the IDataSink interface
    /// </summary>
    public abstract class DataSinkBase : IDataSink
    {
        /// <summary>
        /// Object to lock on when writing
        /// </summary>
        private Object Locker = new Object();

        /// <summary>
        /// Init method of data sink
        /// </summary>
        /// <returns>signal success with true</returns>
        public virtual bool Init()
        {
            return true;
        }

        /// <summary>
        /// Flags successful initialization
        /// </summary>
        public bool IsInitialized { get; set; } = false;

        /// <summary>
        /// Write sensor value to data sink
        /// </summary>
        /// <param name="SensorValue">Tinkerforge Sensor plugin value</param>
        public abstract void HandleValue(MeasurementValue SensorValue);

        /// <summary>
        /// Get the name of data sink class
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        /// <summary>
        /// Shutdown of data sink
        /// </summary>
        public virtual void Shutdown()
        {
        }

        /// <summary>
        /// Lock object for preventing concurrent access to same data sink
        /// </summary>
        protected object WriteLock { get { return Locker; } }
    }
}