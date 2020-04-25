using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace net.derpaul.tf.plugin
{
    internal class WaterGaugeObject
    {
        public string Id { get; set; }
        public string Location { get; set; }
        public string River { get; set; }
        public double Gauge { get; set; }
        public string Unit { get; set; }
        public DateTime Timestamp { get; set; }
        public override string ToString()
        {
            return $"ID [{Id}], Location [{Location}], River [{River}], Gauge [{Gauge}], Unit [{Unit}], Timestamp [{Timestamp}]";
        }

        public void Init(string WaterGaugeDataRaw)
        {
            var dtaprop = JsonConvert.DeserializeObject<List<string>>(WaterGaugeDataRaw);
            Id = dtaprop[WaterGaugeConfig.Instance.IndexID];
            Location = dtaprop[WaterGaugeConfig.Instance.IndexLocation];
            River = dtaprop[WaterGaugeConfig.Instance.IndexRiver];
            var gauge_raw = dtaprop[WaterGaugeConfig.Instance.IndexGauge].Replace("--", "0");
            Gauge = double.Parse(gauge_raw, System.Globalization.CultureInfo.InvariantCulture);
            Unit = dtaprop[WaterGaugeConfig.Instance.IndexUnit];
            Timestamp = DateTime.ParseExact(dtaprop[WaterGaugeConfig.Instance.IndexTimestamp], WaterGaugeConfig.Instance.TimestampFormat, System.Globalization.CultureInfo.InvariantCulture);
        }

        public MeasurementValue GetValue()
        {
            MeasurementValue value = new MeasurementValue(Location, Unit, WaterGaugeConfig.Instance.SortOrder);
            value.Value = Gauge;
            value.Timestamp = Timestamp;
            return value;
        }
    }
}
