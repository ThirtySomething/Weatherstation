# Weatherstation client on RPi

This describes the way how to run the software on a Raspberry Pi running raspbian.

## Prerequisites

* First of all get your Raspi up to date<pre>
sudo apt-get update
sudo apt-get upgrade
</pre>

* Add prerequisites for .NET Core framework<pre>
sudo apt-get install curl libunwind8 gettext
</pre>

* Install the .NET Core runtime<pre>
 curl -sSL -o dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Runtime/release/2.0.0/dotnet-runtime-latest-linux-arm.tar.gz
sudo mkdir -p /opt/dotnet && sudo tar zxf dotnet.tar.gz -C /opt/dotnet
sudo ln -s /opt/dotnet/dotnet /usr/local/bin
</pre>

* Test the .NET Core installation<pre>
dotnet --help
</pre> The result should be something like
    <pre>Usage: dotnet [host-options] [path-to-application]

    path-to-application:
    The path to an application .dll file to execute.

    host-options:
    --additionalprobingpath <path>      Path containing probing policy and assemblies to probe for
    --depsfile <path>                   Path to <application>.deps.json file
    --runtimeconfig <path>              Path to <application>.runtimeconfig.json file
    --fx-version <version>              Version of the installed Shared Framework to use to run the application.
    --roll-forward-on-no-candidate-fx   Roll forward on no candidate shared framework is enabled
    --additional-deps <path>            Path to additonal deps.json file

    Common Options:
    -h|--help                           Displays this help.
    --info                              Displays the host information
    </pre> See also [here](https://blogs.msdn.microsoft.com/david/2017/07/20/setting_up_raspian_and_dotnet_core_2_0_on_a_raspberry_pi/) for more information. Maybe this [here](https://github.com/dotnet/core/blob/master/samples/RaspberryPiInstructions.md) is also necessary.

* Then install the Tinkerforge Brick daemon<pre>
sudo apt-get install libusb-1.0-0 libudev0 pm-utils
wget http://download.tinkerforge.com/tools/brickd/linux/brickd_linux_latest_armhf.deb
sudo dpkg -i brickd_linux_latest_armhf.deb
</pre>

* Cross compile the project for Linux ARMHF - This was somehow a tricky part. There exist no SDK for .NET Core for 32-Bit Linux systems on arm. You need do do a cross compile. Assume you're running the project from Windows, just double-click on <code>Install-RPi.bat</code>. Now the process is started and will take a while. You'll notice the end of the process when the DOS-box window is gone. After that you'll find a new folder <code>RPi-Build</code> in the root of the project. Copy this folder to your Raspi.

## First run

On the first run some config files with default values are created. For fine tuning, you need to crate them by starting the client:<pre>
dotnet Client.dll
</pre>
Execute this command on the Linux command line in the <code>RPi-Build</code> directory you copied over to your Raspi.

## Abort the program

The program is running in an endless loop. To abort the loop, just hit the escape button.

## Configuration

The config files are named like the plugin with <code>Config.config</code> at the end. There are some for DataSource plugins:

* AirPressureConfig.config
* AltitudeConfig.config
* AmbientLightConfig.config
* HumidityConfig.config
* TemperatureConfig.config

These files contains only a sort order how to display them on the Weatherstation LCD Bricklet. Usually you don't have to touch them. Additional there are some for the DataSink plugins:

* FileConfig.config
* LcdConfig.config
* MQTTConfig.config
* SQLiteConfig.config

And the config file for the client application itself.

* ClientConfig.config

For more details how to configure the plugins see the Readme in the project of the plugin. For the client the following settings are available:

* <code>Delay</code> - time interval where the next measurement value is picked
* <code>BrickDaemonIP</code> - The IP of the system where the Tinkerforge Brick daemon is running, usually the same where the master brick is connected to: <code>localhost</code>
* <code>BrickDaemonPort</code> - The port of the Tinkerforge Brick daemon, the default is <code>4223</code>
* <code>PluginPath</code> - The path where the client is looking for datasource and datasink plugins, by default in <code>Plugins</code>
* <code>PluginProductName</code> - Each plugin should have a specified product name to identify them as plugins for this software and to drop off the other DLL files. The default is <code>net.derpaul.tf.plugin</code> and this is valid __FOR ALL__ plugins.

## Remarks

Unfortunately some of the plugins are using NuGet packages. During the deployment the plugins have a different deployment location than the client program. Some of the DLLs will load additional dependencies - but somehow they are not taken from the plugin path. Caused by this the NuGet packages are also added to the Client project. This solved the issue, but it's not a good solution.

Return to [main](./../Readme.md).
