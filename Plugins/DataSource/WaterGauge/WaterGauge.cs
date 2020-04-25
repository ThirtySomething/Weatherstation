using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Tinkerforge;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Class to read values from ambient light sensor
    /// </summary>
    public class WaterGauge : DataSourceBase
    {
        /// <summary>
        /// Measurement unit of sensor
        /// </summary>
        public override string Unit { get; } = "cm";

        /// <summary>
        /// Delay in milli seconds until next measurement value is read
        /// </summary>
        public override int ReadDelay { get; } = WaterGaugeConfig.Instance.ReadDelay;

        /// <summary>
        /// The TF sensor type
        /// </summary>
        public override int SensorType { get; } = -1;

        /// <summary>
        /// Constructor
        /// </summary>
        public WaterGauge()
        {
            IsInitialized = false;
        }

        /// <summary>
        /// Initialize internal TF bricklet
        /// </summary>
        /// <param name="connection">Connection to master brick</param>
        /// <param name="UID">Sensor ID</param>
        /// <returns>signal success with true</returns>
        public override bool Init(IPConnection connection, string UID)
        {
            WaterGaugeConfig.Instance.ShowConfig();
            return true;
        }

        /// <summary>
        /// Read value from sensor and prepare real value
        /// </summary>
        /// <returns>Illuminance or 0.0</returns>
        protected override List<MeasurementValue> RawValues()
        {
            var result = new List<MeasurementValue>();

            using (var wc = new System.Net.WebClient())
            {
                string contents = wc.DownloadString(WaterGaugeConfig.Instance.URL);
                MatchCollection matches = Regex.Matches(contents, WaterGaugeConfig.Instance.RegExpRecord);
                List<string> gaugeStations = JsonConvert.DeserializeObject<List<string>>(WaterGaugeConfig.Instance.StationList);

                foreach (Match match in matches)
                {
                    try
                    {
                        var currentWatermark = new WaterGaugeObject();
                        currentWatermark.Init(match.Captures[0].Value);
                        if (gaugeStations.Contains(currentWatermark.Id))
                        {
                            var value = currentWatermark.GetValue();
                            result.Add(value);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception: {ex.Message}");
                    }
                }
            }

            return result;
        }
    }
}
