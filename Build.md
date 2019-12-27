# Weatherstation

This describes the way how to build and run the software.

## Prerequisites for Raspbian

* First of all get your Raspi up to date <pre>
sudo apt-get update
sudo apt-get upgrade
</pre>

* Add prerequisites for .NET Core framework <pre>
sudo apt-get install curl libunwind8 gettext
</pre>

* Install the .NET Core runtime <pre>
curl -SL -o dotnet.tar.gz https://dotnetcli.blob.core.windows.net/dotnet/Sdk/master/dotnet-sdk-latest-linux-arm.tar.gz
sudo mkdir -p /usr/share/dotnet
sudo tar -zxf dotnet.tar.gz -C /usr/share/dotnet
sudo ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet
</pre>

* Test the .NET Core installation <pre>
dotnet --help
</pre>
        The result should be something like <pre>
        Usage: dotnet [host-options] [path-to-application]

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
</pre>

See also [here](https://blogs.msdn.microsoft.com/david/2017/07/20/setting_up_raspian_and_dotnet_core_2_0_on_a_raspberry_pi/) for more information. Maybe this [here](https://github.com/dotnet/core/blob/master/samples/RaspberryPiInstructions.md) is also necessary.

## Common prerequisites

* Download and install the [Tinkerforge Brick daemon][TFBD] for the operating system where you will run the device and connect the weather station to. For Raspbian this looks for example <pre>
sudo apt-get install libusb-1.0-0 libudev0 pm-utils
wget http://download.tinkerforge.com/tools/brickd/linux/brickd_linux_latest_armhf.deb
sudo dpkg -i brickd_linux_latest_armhf.deb
</pre>

* Install a MQTT broker somewhere. In my case I'm running [Mosquitto](https://mosquitto.org/) on my Synology NAS.

## Build the project

* The complete code can be build on any Windows system. There is a cross compile possible and everything is prepared in the `buildscript.bat` script. Possible options for the build script are
  * `linux-arm`
  * `linux-x64`
  * `win-x64`

* The build process will build the device as well as the remote device and all available plugins for the given destination/architecture.

* Compile the code for the desired destination(s).

* After the build process is finished, copy the folder `./build/linux-arm/` to your Raspi.

* For the remote device you need to remove the following plugins from the `Plugins` folder:
  * `Lcd.dll` - Remove this because on the remote device you usually don't have the Tinkerforge display of the weather station.
  * `MQTT.dll` - Remove this unless you want to re-publish the data to another broker/topic. Don't remove `M2Mqtt.dll` because it's used by the remote device to deal with MQTT.

## First run

On the first run some config files with default values are created. Start the device <pre>
dotnet device.dll
</pre>
or the remote device <pre>
dotnet RemoteDevice.dll
</pre>
Execute this commands on the command line in the `build` path of your architecture. Then abort the loop by pressing the escape button. In the next step configure each `*.config` file to fit your needs. For further details see the [plugins](./Plugins/Readme.md) documentation.

## Abort the program

Both programs, the device as well the remote device, are running in an endless loop. To abort the loop, just hit the escape button.

## Configuration

The config files are named like the plugins with `Config.config` at the end. See the [plugins](./Plugins/Readme.md) and their configs for more details.

The description for configuring the [device](./Device/Readme.md) or the [remote device](./RemoteDevice/Readme.md) can be found in their respective directories.

## Caveats

* You cannot run the device and the remote device at one machine using the same plugin directory. The plugins cannot be used simultaneously by the device and the remote device. If you want to run both on the same machine, you need to have two plugin directories and to configure one for the device and the other for the remote device.

* Some of the plugins are using NuGet packages. During the deployment the plugins have a different deployment location than the device program. Some of the DLLs will load additional dependencies - but somehow they are not taken from the plugin path. Caused by this the NuGet packages are also added to the device/remote device project. This solved the issue, but it's not a good solution.

Return to [main](./Readme.md).

[TFBD]:https://www.tinkerforge.com/en/doc/Software/Brickd.html