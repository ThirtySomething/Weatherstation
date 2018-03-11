namespace net.derpaul.tf
{
    /// <summary>
    /// Config settings of ambient light sensor
    /// </summary>
    public class AmbientLightConfig : ConfigLoader<AmbientLightConfig>, ConfigSaver
    {
        /// <summary>
        /// Sort order
        /// </summary>
        public int SortOrder = 3;
    }
}