# Weatherstation on RPi

This describes the way how to run the software on a Raspberry Pi running raspbian.

## Prerequisites

* First of all get your Raspi up to date<pre>
sudo apt-get update
sudo apt-get upgrade
</pre>

* Add prerequisites for .NET Core framework<pre>
sudo apt-get install curl libunwind8 gettext
</pre>

* Then install the Tinkerforge Brick daemon<pre>
sudo apt-get install libusb-1.0-0 libudev0 pm-utils
wget http://download.tinkerforge.com/tools/brickd/linux/brickd_linux_latest_armhf.deb
sudo dpkg -i brickd_linux_latest_armhf.deb
</pre>

* Cross compile the project for Linux ARMHF - This was somehow a tricky part. There exist no SDK for .NET Core for 32-Bit Linux systems on arm. You need do do a cross compile. Assume you're running the project from Windows, just double-click on <code>build-rpi.bat</code>. Now the process is started and will take a while. You'll notice the end of the process when the DOS-box window is gone. After that you'll find a new folder <code>RPi-Build</code> in the root of the project. Copy this folder to your Raspi.

## Configuration

On the first run the plugins are loaded and initialized. During this process config files with default settings are created. To abort the endless loop, just hit the escape key and be patient. Ater that modify at least the file <code>MQTTConfig.config</code> to adress the correct MQTT broker.
