namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Config settings of air pressure sensor
    /// </summary>
    public class AirPressureConfig : ConfigLoader<AirPressureConfig>, IConfigSaver
    {
        /// <summary>
        /// Sort order for air pressure
        /// </summary>
        public int SortOrder = 1;
    }
}