# Client

The client is running on the device where the weatherstations is connected directly via USB. The client will collect the data from the sensors ([data source plugins](./../Plugins/DataSource/Readme.md)) and will submit the data to the loaded output plugins ([data sink plugins](./../Plugins/DataSink/Readme.md)).

## Details

The client reads data from [data source plugins](./../Plugins/DataSource/Readme.md) and sends it to [data sink plugins](./../Plugins/DataSink/Readme.md).

__To abort the endless loop of picking up measurement values just hit escape.__

Return to [main](./../Readme.md).

## ClientConfig.config

For more details how to configure the plugins see the Readme in the project of the plugin. For the client the following settings are available:

* `Delay` - time interval where the next measurement value is picked
* `BrickDaemonIP` - The IP of the system where the Tinkerforge Brick daemon is running, usually the same as where the master brick is connected to: `127.0.0.1`
* `BrickDaemonPort` - The port of the Tinkerforge Brick daemon, the default is `4223`
* `PluginPath` - The path where the client is looking for datasource and datasink plugins, by default in a subdirectory called `Plugins`
* `PluginProductName` - Each plugin should have a specified product name to identify them as plugins for this software and to drop off the other DLL files. The default is `net.derpaul.tf.plugin.[name]` and this is valid __FOR ALL__ plugins.

## Remarks

See also the [server documentation](./../Server/Readme.md). The description to install the client on a Raspi can be found [here](./Install-RPi.md).
