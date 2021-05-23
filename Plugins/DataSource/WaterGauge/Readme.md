# WaterGauge

The WaterGauge plugin reads the water gauge of rivers of Baden-Württemberg/Germany. At least it's scraping the data from the [website](https://www.hvz.baden-wuerttemberg.de).

ATTENTION: Never rely on these values! This plugin is just an example for reading data form another source than a Tinkerforge Sensor.

Return to [main](./../Readme.md).

## WaterGaugeConfig.config

For the WaterGauge plugin the following settings are available:

* `ReadDelay` - The delay in milli seconds until next measurement values are read, default is `3600000` - one hour
* `SortOrder` - The sort order on the Tinkerforge lcd display, default is `100` for not displaying the data - see also [LcdConfig.config](./../../DataSink/Lcd/Readme.md) for details
* `URL` - The URL to read/scrape the data from
* `RegExpRecord` - The regular expression to read one data row, default i `(\['.*\])`
* `IndexID` - The index of the ID in the data row object, default is `0`
* `IndexLocation` - The index of the location (aka city/town) in the data row object, default is `1`
* `IndexRiver` - The index of the river in the data row object, default is `2`
* `IndexGauge` - The index of the gauge in the data row object, default is `4`
* `IndexUnit` - The index of the unit in the data row object, default is `5`
* `IndexTimestamp` - The index of the timestamp in the data row object, default is `12`
* `TimestampFormat` - The format of the timestamp in the data row object, default is `dd.MM.yyyy HH:mm`
* `StationList` - The stations where to pick the gauge from, default is `['00034', '00163']`
