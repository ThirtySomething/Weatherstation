# Weatherstation

This project will address a [Tinkerforge weather station][TFURL]. There are
  - The device for reading the data of the weather station.
  - The remote device for dealing with the data in form of saving them, historize them and compress them somehow.
  - The UI for displaying the data processed by the remote device.

## General information

[![License: LGPL v3][lgpl_license_badge]][lgpl_license]
[![Size:][repo_size_badge]][repo_size]
[![Language: C#][csharp_lang_badge]][csharp_lang]

**Weatherstation** is distributed under the terms of the **GNU LESSER GENERAL PUBLIC LICENSE**, version 3.0. The text of the license is included in the file [`LICENSE.TXT`](https://github.com/ThirtySomething/Weatherstation/blob/master/LICENSE.TXT "LGPL-3.0") in the project root.

## Motivation

As training for my developer skills, I'm using my [Tinkerforge weather station][TFURL] for excercising.

## Prerequisites

You need to have some software installed to compile this project. There are

- The [Tinkerforge Brick Daemon][TFBrickDaemon] to run the software.
- The [Tinkerforge Brick Viewer][TFBrickViewer] to update the firmwares.
- The [MS .net Core 3.1 SDK][DotNet31SDK] for compiling/extend the software.

## Device

See the [device documentation](./Device/Readme.md) for more details.

## Remote device

See the [remote device documentation](./RemoteDevice/Readme.md) for more details.

## Plugins

See the [plugin documentation](./Plugins/Readme.md) for more details.

## Build instructions

To build and run the software, see [here](./Build.md) for more details.

## Schema overview

![Schema overview](./Documentation/Diagram.png)

## ERM overview

![ERM overview](./Documentation/DataModel.png)

## ToDos

- Create MariaDB plugin based on experience of SQLite plugin.
- Get MQTT plugin working again using IP and port.
- Improve MQTT plugin to handle not acknowledged data.
- Remember to remove [M2MqttDotnetCore][NGMQTT] NuGet package at device before publish
- Implement remote device part, consists of
  - Writing data to database (MySQL, MariaDB, ...)
  - Implement a [swinging door algorithm][SDoor] for historizing/compressing the data.
  - Create a HTML frontend with various information
    - Data of current values
    - Historized data
    - Graphics

## Notes to myself
- [Data compression on Techincal University Dresden][TUDresden]
- [Swinging Door Algorithm@PiSquare][SwingingDoorPiSquare]
- [Swinging Door Algorithm@OSI PI][SwingingDoorOsiPi]
- [Swinging Door Implementation in C#][SwingingDoorImpl]
- Is the swinging door a good algorithm to be used for weather data?
- What about [rrdtool]?
- What about [prometheus]?
- What about [opentsdb]?

[NGMQTT]: https://www.nuget.org/packages/M2MqttDotnetCore/
[SDoor]: https://support.industry.siemens.com/cs/document/109739594/komprimierung-von-prozesswertarchiven-mit-dem-swinging-door-algorithmus-in-pcs-7?dti=0&lc=de-WW
[TFBrickDaemon]: https://www.tinkerforge.com/de/doc/Downloads.html
[TFBrickViewer]: https://www.tinkerforge.com/de/doc/Downloads.html
[TFURL]: https://www.tinkerforge.com/en/doc/Kits/Weatherstation/Weatherstation.html
[opentsdb]: http://opentsdb.net/
[prometheus]: https://prometheus.io/
[rrdtool]: https://oss.oetiker.ch/rrdtool/
[TUDresden]: http://www.et.tu-dresden.de/ifa/uploads/media/PIV006-Archiv.pdf
[SwingingDoorPiSquare]: https://pisquare.osisoft.com/thread/7566
[SwingingDoorOsiPi]: https://osipi.wordpress.com/tag/swinging-door-algorithm/
[SwingingDoorImpl]: https://www.hackerboard.de/threads/c-gesucht-implementierung-des-swinging-door-algorithmus.50448/
[DotNet31SDK]: https://dotnet.microsoft.com/download/dotnet-core/3.1
[EFCore]: https://github.com/aspnet/EntityFrameworkCore

[lgpl_license]: http://www.gnu.org/licenses/lgpl-3.0
[lgpl_license_badge]: https://img.shields.io/badge/License-LGPL%20v3-blue.svg
[repo_size_badge]: https://img.shields.io/github/repo-size/ThirtySomething/Weatherstation
[repo_size]: https://github.com/ThirtySomething/Weatherstation
[csharp_lang_badge]: https://img.shields.io/badge/language-CSharp-blue.svg
[csharp_lang]: https://en.wikipedia.org/wiki/C_Sharp_(programming_language)
