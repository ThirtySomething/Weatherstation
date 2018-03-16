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

The client project part is responsible for dealing with the data of the weatherstation. See [here](./Client/Readme.md) fore more details.

### ToDos

- Remember to remove [M2MqttDotnetCore][NGMQTT] NuGet package at client before publish
- Implement server part, consists of
  - MQTT client subscribing to the same topic as MQTT plugin
  - Writing data to database (MySQL, MariaDB, SQLite, ...)
  - Implement a [swinging door algorithm][SDoor] for historizing/compressing the data.
  - Create a HTML frontend with various information
    - Data of current values
    - Historized data
    - Graphics

### URLs

#### Swinging door links
- http://www.et.tu-dresden.de/ifa/uploads/media/PIV006-Archiv.pdf
- https://pisquare.osisoft.com/thread/7566
- https://osipi.wordpress.com/tag/swinging-door-algorithm/
- https://www.hackerboard.de/code-kitchen/50448-c-gesucht-implementierung-des-swinging-door-algorithmus.html

[SDoor]:https://support.industry.siemens.com/cs/document/109739594/komprimierung-von-prozesswertarchiven-mit-dem-swinging-door-algorithmus-in-pcs-7?dti=0&lc=de-WW
[TFURL]:https://www.tinkerforge.com/en/doc/Kits/WeatherStation/WeatherStation.html
[NGMQTT]:https://www.nuget.org/packages/M2MqttDotnetCore/