namespace net.derpaul.tf
{
    /// <summary>
    /// Config settings of barometer sensor
    /// </summary>
    public class AltitudeConfig : ConfigLoader<AltitudeConfig>, ConfigSaver
    {
        /// <summary>
        /// Sort order for altitude
        /// </summary>
        public int SortOrder = 4;
    }
}