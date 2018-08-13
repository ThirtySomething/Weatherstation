# Device

The device is running on the device where the weatherstations is connected directly via USB. The device will collect the data from the sensors ([data source plugins](./../Plugins/DataSource/Readme.md)) and will submit the data to the loaded output plugins ([data sink plugins](./../Plugins/DataSink/Readme.md)).

## Details

The device reads data from [data source plugins](./../Plugins/DataSource/Readme.md) and sends it to [data sink plugins](./../Plugins/DataSink/Readme.md).

__To abort the endless loop of picking up measurement values just hit escape.__

Return to [main](./../Readme.md).

## DeviceConfig.config

For more details how to configure the plugins see the Readme in the project of the plugin. For the device the following settings are available:

* `Delay` - time interval where the next measurement value is picked.
* `BrickDaemonIP` - The IP of the system where the Tinkerforge Brick daemon is running, usually the same as where the master brick is connected to: `127.0.0.1`.
* `BrickDaemonPort` - The port of the Tinkerforge Brick daemon, the default is `4223`.
* `PluginPath` - The path where the device is looking for datasource and datasink plugins, by default in a subdirectory called `Plugins`.

## Remarks

See also the [remote device documentation](./../RemoteDevice/Readme.md). The description to install the device on a Raspi can be found [here](./Install-RPi.md).
