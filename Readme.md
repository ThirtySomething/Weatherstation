# Weatherstation

This project will address a [Tinkerforge weather station][TFURL]. It's splittet into two parts:
  - The client for reading the data of the weather station.
  - The server for dealing with the data in form of history and diagrams.

## General information

[![License: LGPL v3](https://img.shields.io/badge/License-LGPL%20v3-blue.svg)](http://www.gnu.org/licenses/lgpl-3.0 "LGPL-3.0")

**Weatherstation** is distributed under the terms of the **GNU LESSER GENERAL PUBLIC LICENSE**, version 3.0. The text of the license is included in the file [<code>LICENSE.TXT</code>](https://github.com/ThirtySomething/Weatherstation/blob/master/LICENSE.TXT "LGPL-3.0") in the project root.

## Motivation

As training for my developer skills, I'm using my [Tinkerforge weather station][TFURL] for excercising.

## Client

The client project part is responsible for dealing with the data of the weatherstation. 

### Implementation details

- The Tinkerforge library is pulled as [NuGet Package][TFNuGet].
- A mechanism to simplify the handling of config files is implemented:
  - The <code>ConfigSaver</code> interface is mandatory for each config settings.
  - The <code>ConfigLoader</code> generic, also mandatory for each config settings.
  - See <code>ClientConfig</code> how to apply the previous mentioned parts.
- A plugin system, based on this article of [Christoph Gattnar][Plugin]. There are two types of plugins:
  - <code>ISensor</code> plugins are responsible for reading data of the sensors. They are data producers.
  - <code>IDataSink</code> plugins are responsible for working with the data. They are data consumers.

### ToDos

- Remember to remove <code>M2MqttDotnetCore</code> NuGet package at client before publish
- Implement server part, consists of
  - MQTT client subscribing to the same topic as MQTT plugin
  - Writing data to database (MySQL, MariaDB, SQLite, ...)
  - Implement a [swinging door algorithm][SDoor] for historizing/compressing the data.
  - Create a HTML frontend with various information
    - Data of current values
    - Historized data
    - Graphics

### URLs

#### Swinging door
- http://www.et.tu-dresden.de/ifa/uploads/media/PIV006-Archiv.pdf
- https://pisquare.osisoft.com/thread/7566
- https://osipi.wordpress.com/tag/swinging-door-algorithm/
- https://www.hackerboard.de/code-kitchen/50448-c-gesucht-implementierung-des-swinging-door-algorithmus.html

[Plugin]:https://code.msdn.microsoft.com/windowsdesktop/Creating-a-simple-plugin-b6174b62
[SDoor]:https://support.industry.siemens.com/cs/document/109739594/komprimierung-von-prozesswertarchiven-mit-dem-swinging-door-algorithmus-in-pcs-7?dti=0&lc=de-WW
[TFNuGet]:https://www.nuget.org/packages/Tinkerforge/
[TFURL]:https://www.tinkerforge.com/en/doc/Kits/WeatherStation/WeatherStation.html

