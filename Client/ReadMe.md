# TFPlugin

## General information

[![License: LGPL v3](https://img.shields.io/badge/License-LGPL%20v3-blue.svg)](http://www.gnu.org/licenses/lgpl-3.0 "LGPL-3.0")

**TFPlugin** is distributed under the terms of the **GNU LESSER GENERAL PUBLIC LICENSE**, version 3.0. The text of the license is included in the file [<code>LICENSE.TXT</code>](https://github.com/ThirtySomething/YAIP/blob/master/LICENSE.TXT "LGPL-3.0") in the project root.

## Motivation

As training for my C# skills, I'm using my [Tinkerforge weather station][TFURL] for excercising.

## Implementation details

- The plugin system is based on this article of [Christoph Gattnar][Plugin].
- Two kind of plugins are available:
  - Each Tinkerforge sensor is a plugin of type <code>TFSensor</code>. These plugins produce the data. 
  - There is also a plugin of type <code>TFDataSink</code> available. These plugins consume the data.
- The Tinkerforge library is pulled as [NuGet Package][TFNuGet].
- An abstract base class is used for the <code>TFSensor</code> plugins to catch exception of Tinkerforge.
- A mechanism to simplify the handling of config files is implemented:
  - The <code>ConfigSaver</code> interface is mandatory for each config settings.
  - The <code>ConfigLoader</code> generic, also mandatory for each config settings.
  - See <code>TFPluginCoreConfig</code> how to apply the previous mentioned parts.

## ToDo's

- Add data sink
  - A remote data sink addressed by MQTT for example
  - Avoid double call of <code>TFHandler::IdentifySensorsCallBack</code> - find best place for unregister callback

[Plugin]:https://code.msdn.microsoft.com/windowsdesktop/Creating-a-simple-plugin-b6174b62
[TFNuGet]:https://www.nuget.org/packages/Tinkerforge/
[TFURL]:https://www.tinkerforge.com/en/doc/Kits/WeatherStation/WeatherStation.html
