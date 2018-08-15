# Common

Here is common used software stuff collected. This library will be used from the device as well as from the remote device.

## Details

Common stuff is

* The config loader, consisting of
  * The generic class `ConfigLoader.cs`
  * The interface for `IConfigObjects.cs`
* The definition of an output plugin `IDataSink.cs`
* The definition of an input plugin `IDataSource.cs`
* The common used object of a `MeasurementValue.cs`
* A generic `PluginLoader.cs`
* An abstract base class `SensorBase.cs` for the datasource plugins
* An abstract base class `TFSensor.cs` to access the Tinkerforge sensors
* An utility class `TFUtils.cs` containing some helper stuff