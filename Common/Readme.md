# Common

Here is common used software stuff collected. This library will be used from the `datacollector` as well as from the `remotedevice`.

## Details

Common stuff is

* The config loader, consisting of
  * The generic class `ConfigLoader.cs`
  * The interface for `IConfigObjects.cs`
* The definition of an output plugin `IDataSink.cs`
* An abstract base class for all data sinks `DataSinkBase.cs`
* The definition of an input plugin `IDataSource.cs`
* An abstract base class for all data sources `DataSourceBase.cs`
* The common used object of a `MeasurementValue.cs`
* A generic `PluginLoader.cs`
* An abstract base class `TFSensor.cs` to access the Tinkerforge sensors
* A utility class `TFUtils.cs` containing some helper stuff
