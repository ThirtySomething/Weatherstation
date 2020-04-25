namespace net.derpaul.tf.plugin
{
    /// <summary>
    /// Configuration settings of ambient light sensor
    /// </summary>
    public class WaterGaugeConfig : ConfigLoader<WaterGaugeConfig>, IConfigObject
    {
        /// <summary>
        /// To set default values
        /// </summary>
        public void SetDefaults()
        {
            // 1 hour, 60 minutes, 60 seconds, 1000 milliseconds
            // 1 * 60 * 60 * 1000 = 3600000
            ReadDelay = 3600000;
            SortOrder = -1;
            URL = "https://www.hvz.baden-wuerttemberg.de/js/hvz-data-peg-db.js";
            RegExpRecord = @"(\['.*\])";
            IndexID = 0;
            IndexLocation = 1;
            IndexRiver = 2;
            IndexGauge = 4;
            IndexUnit = 5;
            IndexTimestamp = 12;
            TimestampFormat = "dd.MM.yyyy HH:mm";
            StationList = "['00034', '00163']";
        }

        /// <summary>
        /// Delay in milli seconds until next measurement value is read
        /// </summary>
        public int ReadDelay { get; set; }

        /// <summary>
        /// Sort order
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// URL to read for water gauges
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Regular expression for one data row
        /// </summary>
        public string RegExpRecord { get; set; }

        /// <summary>
        /// Index to unique ID of water gauge record
        /// </summary>
        public int IndexID { get; set; }

        /// <summary>
        /// Index to name of city/town of water gauge record
        /// </summary>
        public int IndexLocation { get; set; }

        /// <summary>
        /// Index to river of water gauge record
        /// </summary>
        public int IndexRiver { get; set; }

        /// <summary>
        /// Index to gauge of water gauge record
        /// </summary>
        public int IndexGauge { get; set; }

        /// <summary>
        /// Index to unit of water gauge record
        /// </summary>
        public int IndexUnit { get; set; }

        /// <summary>
        /// Index to timestamp of water gauge record
        /// </summary>
        public int IndexTimestamp { get; set; }

        /// <summary>
        /// Format of timestamp of water gauge record
        /// </summary>
        public string TimestampFormat { get; set; }

        /// <summary>
        /// The ids to pick gauges from as JSON string
        /// </summary>
        public string StationList { get; set; }
    }
}