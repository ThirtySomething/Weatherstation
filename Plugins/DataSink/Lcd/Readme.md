## Lcd

The lcd plugin writes measurement values to the lcd display of the weather station.

Return to [main](./../Readme.md).

## LcdConfig.config

For the lcd plugin the following settings are available:

* <code>BrickDaemonIP</code> - The IP of the system where the Tinkerforge brick daemon is running. Usually the same system where the weather station is connected to: <code>127.0.0.1</code>
* <code>BrickDaemonPort</code> - The port of the brick daemon, the default is <code>4223</code>
* <code>TimestampFormat</code> - The format of the timestamp printed to the lcd display, the default is <code>dd.MM.yyyy  HH:mm:ss</code>