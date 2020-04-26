using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Representation of one water gauge data row
    /// </summary>
    internal class WaterGaugeObject
    {
        /// <summary>
        /// Internal ID of measurement station/river
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// City/town of measurement station
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// River of water gauge
        /// </summary>
        public string River { get; set; }

        /// <summary>
        /// Gauge value of river at given location
        /// </summary>
        public double Gauge { get; set; }

        /// <summary>
        /// Unit of gauge
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Timestamp of gauge value, default is DateTime.now
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// For debugging purposes
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"ID [{Id}], Location [{Location}], River [{River}], Gauge [{Gauge}], Unit [{Unit}], Timestamp [{Timestamp}]";
        }

        /// <summary>
        /// Raw gauge data as JSON string
        /// </summary>
        /// <param name="WaterGaugeDataRaw"></param>
        public void Init(string WaterGaugeDataRaw)
        {
            // Convert string to array
            var dtaprop = JsonConvert.DeserializeObject<List<string>>(WaterGaugeDataRaw);
            // Retrieve data
            Id = dtaprop[WaterGaugeConfig.Instance.IndexID];
            Location = dtaprop[WaterGaugeConfig.Instance.IndexLocation];
            River = dtaprop[WaterGaugeConfig.Instance.IndexRiver];
            var gauge_raw = dtaprop[WaterGaugeConfig.Instance.IndexGauge].Replace("--", "0");
            Gauge = double.Parse(gauge_raw, System.Globalization.CultureInfo.InvariantCulture);
            Unit = dtaprop[WaterGaugeConfig.Instance.IndexUnit];
            if (dtaprop[WaterGaugeConfig.Instance.IndexTimestamp].Equals("--"))
            {
                Timestamp = DateTime.Now;
            }
            else
            {
                Timestamp = DateTime.ParseExact(dtaprop[WaterGaugeConfig.Instance.IndexTimestamp], WaterGaugeConfig.Instance.TimestampFormat, System.Globalization.CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Create a single measurement value
        /// </summary>
        /// <returns></returns>
        public MeasurementValue GetValue()
        {
            MeasurementValue value = new MeasurementValue("", Location, Unit, WaterGaugeConfig.Instance.SortOrder);
            value.Value = Gauge;
            value.Timestamp = Timestamp;
            return value;
        }
    }
}
