namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Configuration settings of MQTT plugin
    /// </summary>
    public class MQTTConfig : ConfigLoader<MQTTConfig>, IConfigObject
    {
        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            BrokerIP = "test.mosquitto.org";
            ClientID = "WeatherMQTTDevice";
            TopicData = "/tinkerforge/weatherstation/dta";
            TopicAcknowledge = "/tinkerforge/weatherstation/ack";
            TimerDelay = 10000;
        }

        /// <summary>
        /// IP of MQTT broker to connect to
        /// </summary>
        public string BrokerIP { get; set; }

        /// <summary>
        /// Client ID
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// Topic to publish data to
        /// </summary>
        public string TopicData { get; set; }

        /// <summary>
        /// Topic to recieve handshake information
        /// </summary>
        public string TopicAcknowledge { get; set; }

        /// <summary>
        /// Timer interval to check the acknowledge list
        /// </summary>
        public int TimerDelay { get; set; }
    }
}