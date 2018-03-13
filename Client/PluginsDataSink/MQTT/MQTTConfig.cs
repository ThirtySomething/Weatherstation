﻿namespace net.derpaul.tf
{
    public class MQTTConfig : ConfigLoader<MQTTConfig>, ConfigSaver
    {
        /// <summary>
        /// IP of MQTT broker to connect to
        /// </summary>
        public string BrokerIP { get; set; } = "127.0.0.1";

        /// <summary>
        /// Port of MQTT broker to connect to
        /// </summary>
        public int BrokerPort { get; set; } = 1883;
    }
}