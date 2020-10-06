# Data Sink Plugins

Several data sink plugins exist. Each of them has its (possible) own configuration. Currently the following data sinks exists:

* [Console](./Console/Readme.md) - Simple print the measurement value information to the console
* [Database](./Database/Readme.md) - Write the measurement value information to a database
* [File](./File/Readme.md) - Write the measurement value information to a file
* [LCD](./Lcd/Readme.md) - Print the measurement value information to the LCD bricklet of the weather station
* [MQTT](./MQTT/Readme.md) - Publish the measurement value information to a MQTT broker

Return to [main](./../../Readme.md).

## Remarks

* The data sink plugins can be used on `datacollector` side as well as on `remotedevice` side.
* The implementation offers a `lock object` to prevent concurrent write access by the threads of the data source plugins.
