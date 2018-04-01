namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Class to read altitude from barometer sensor
    /// </summary>
    public class Altitude : Barometer
    {
        /// <summary>
        /// Measurement unit of sensor
        /// </summary>
        public override string Unit { get; } = "m";

        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Altitude or 0.0</returns>
        protected override MeasurementValue ValueGetRaw()
        {
            MeasurementValue result = new MeasurementValue(Name, Unit, AltitudeConfig.Instance.SortOrder);

            if (_Bricklet != null)
            {
                int altitudeRaw = _Bricklet.GetAltitude();
                result.Value = altitudeRaw / 100.0;
            }

            return result;
        }
    }
}