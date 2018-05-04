namespace net.derpaul.tf
{
    /// <summary>
    /// Configuration settings of ambient light sensor
    /// </summary>
    public class AmbientLightConfig : ConfigLoader<AmbientLightConfig>, IConfigSaver
    {
        /// <summary>
        /// Sort order
        /// </summary>
        public int SortOrder = 3;
    }
}