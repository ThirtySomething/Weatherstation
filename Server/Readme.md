## Server

The server is responsible for several different things:

- Picking up the data from via MQTT
- Push read data to all found data sinks

__To abort the endless loop of picking measurement values just hit escape.__

### Implementation details

- The MQTT library is also pulled as [NuGetPackage][NGMQTT].
- <code>IDataSink</code> plugins are responsible for working with the data. They are data consumers and the same as for the client.
- A plugin system, based on this article of [Christoph Gattnar][Plugin]. There are two types of plugins:
  - <code>IDataSource</code> plugins are responsible for reading data of the sensors. They are data producers.
  - <code>IDataSink</code> plugins are responsible for working with the data. They are data consumers.
- See also the [client documentation](./../Client/Readme.md)

[NGMQTT]:https://www.nuget.org/packages/M2MqttDotnetCore/
[Plugin]:https://code.msdn.microsoft.com/windowsdesktop/Creating-a-simple-plugin-b6174b62