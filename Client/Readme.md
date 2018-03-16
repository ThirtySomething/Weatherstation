## Client

The client is responsible for several different things:
- Picking up the data from the weather station itself - each hardware sensor has its own plugin
  - Ambient light sensor
  - Barometer sensor
  - Humidity sensor
  
  As barometer sensor is special, this plugin contains several sensor data:
  - Air pressure
  - Altitude
  - Temperature

- Sending the data to data sinks as there are:
  - Console
  - Tinkerforge LCD display as part of the weather station
  - MQTT - publish the data to somewhere else

### Implementation details

- The Tinkerforge library is pulled as [NuGet Package][NGTinkerForge].
- The MQTT library is also pulled as [NuGetPackage][NGMQTT].
- A mechanism to simplify the handling of config files is implemented:
  - The <code>ConfigSaver</code> interface is mandatory for each config settings.
  - The <code>ConfigLoader</code> generic, also mandatory for each config settings.
  - See <code>ClientConfig</code> how to apply the previous mentioned parts.
- A plugin system, based on this article of [Christoph Gattnar][Plugin]. There are two types of plugins:
  - <code>ISensor</code> plugins are responsible for reading data of the sensors. They are data producers.
  - <code>IDataSink</code> plugins are responsible for working with the data. They are data consumers.

[NGTinkerForge]:https://www.nuget.org/packages/Tinkerforge/
[NGMQTT]:https://www.nuget.org/packages/M2MqttDotnetCore/
[Plugin]:https://code.msdn.microsoft.com/windowsdesktop/Creating-a-simple-plugin-b6174b62