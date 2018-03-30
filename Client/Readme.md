## Client

The client reads the data from [data source plugins](./../Plugins/DataSource/Readme.md) and sends the data to [data sink plugins](./../Plugins/DataSink/Readme.md).

__To abort the endless loop of picking measurement values just hit escape.__

Return to [main](./../Readme.md).

## ClientConfig.config

For more details how to configure the plugins see the Readme in the project of the plugin. For the client the following settings are available:

* <code>Delay</code> - time interval where the next measurement value is picked
* <code>BrickDaemonIP</code> - The IP of the system where the Tinkerforge Brick daemon is running, usually the same where the master brick is connected to: <code>localhost</code>
* <code>BrickDaemonPort</code> - The port of the Tinkerforge Brick daemon, the default is <code>4223</code>
* <code>PluginPath</code> - The path where the client is looking for datasource and datasink plugins, by default in <code>Plugins</code>
* <code>PluginProductName</code> - Each plugin should have a specified product name to identify them as plugins for this software and to drop off the other DLL files. The default is <code>net.derpaul.tf.plugin</code> and this is valid __FOR ALL__ plugins.

### Remarks

See also the [server documentation](./../Server/Readme.md). The description to install the client on a Raspi can be found [here](./Install-RPi.md).
