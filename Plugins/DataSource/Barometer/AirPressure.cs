namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Class to read air pressure from barometer sensor
    /// </summary>
    public class AirPressure : Barometer
    {
        /// <summary>
        /// Measurement unit of sensor
        /// </summary>
        public override string Unit { get; } = "mb";

        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Air pressure or 0.0</returns>
        protected override MeasurementValue RawValue()
        {
            MeasurementValue returnValue = new MeasurementValue(Name, Unit, AirPressureConfig.Instance.SortOrder);

            if (Bricklet != null)
            {
                int airPressureRaw = Bricklet.GetAirPressure();
                returnValue.Value = airPressureRaw / 1000.0;
            }

            return returnValue;
        }
    }
}