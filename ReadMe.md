# TFPlugin

## General information

[![License: LGPL v3](https://img.shields.io/badge/License-LGPL%20v3-blue.svg)](http://www.gnu.org/licenses/lgpl-3.0 "LGPL-3.0")

**TFPlugin** is distributed under the terms of the **GNU LESSER GENERAL PUBLIC LICENSE**, version 3.0. The text of the license is included in the file [<code>LICENSE.TXT</code>](https://github.com/ThirtySomething/YAIP/blob/master/LICENSE.TXT "LGPL-3.0") in the project root.

## Motivation

As training for my C# skills, I'm using my [Tinkerforge weather station][TFURL] for excercising.

## Implementation details

- Each sensor should be a plugin. The plugin system is based on this article of [Christoph Gattnar][Plugin].
- The Tinkerforge parts are pulled as [NuGet Package][TFNuGet].
- Using abstract base class to catch exception of Tinkerforge.

## ToDo's

- Add data sink
   - A TF device such as the display of the weather station
   - A remote data sink addressed by MQTT for example

[Plugin]:https://code.msdn.microsoft.com/windowsdesktop/Creating-a-simple-plugin-b6174b62
[TFNuGet]:https://www.nuget.org/packages/Tinkerforge/
[TFURL]:https://www.tinkerforge.com/en/doc/Kits/WeatherStation/WeatherStation.html
