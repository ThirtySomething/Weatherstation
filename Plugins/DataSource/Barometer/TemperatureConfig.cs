namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Config settings of temperatur sensor
    /// </summary>
    public class TemperatureConfig : ConfigLoader<TemperatureConfig>, IConfigSaver
    {
        /// <summary>
        /// Sort order for temperature
        /// </summary>
        public int SortOrder = 0;
    }
}