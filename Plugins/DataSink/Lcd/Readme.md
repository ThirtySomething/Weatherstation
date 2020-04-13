# Lcd

The lcd plugin writes measurement values to the lcd display of the weather station.

Return to [main](./../Readme.md).

## LcdConfig.config

For the lcd plugin the following settings are available:

* `BrickDaemonIP` - The IP of the system where the Tinkerforge brick daemon is running. Usually the same system where the weather station is connected to: `127.0.0.1`
* `BrickDaemonPort` - The port of the brick daemon, the default is `4223`
* `TimestampFormat` - The format of the timestamp printed to the lcd display, the default is `dd.MM.yyyy  HH:mm:ss`
