namespace net.derpaul.tf
{
    /// <summary>
    /// Configuration settings of altitude sensor
    /// </summary>
    public class AltitudeConfig : ConfigLoader<AltitudeConfig>, IConfigSaver
    {
        /// <summary>
        /// Sort order for altitude
        /// </summary>
        public int SortOrder = 4;
    }
}