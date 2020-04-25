using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tinkerforge;

namespace net.derpaul.tf
{
    /// <summary>
    /// Class to deal with collections of plugins of type IDataSource and IDataSink
    /// </summary>
    internal class PluginHandler
    {
        /// <summary>
        /// TF host
        /// </summary>
        private string Host { get; set; }

        /// <summary>
        /// TF port
        /// </summary>
        private int Port { get; set; }

        /// <summary>
        /// The path to the plugins
        /// </summary>
        private string PluginPath { get; set; }

        /// <summary>
        /// Internal connection to TF master brick
        /// </summary>
        private IPConnection TFConnection { get; set; }

        /// <summary>
        /// Internal flag for successful connection
        /// </summary>
        private bool Connected { get; set; }

        /// <summary>
        /// List of data source plugins
        /// </summary>
        private List<IDataSource> DataSourcePlugins { get; set; }

        /// <summary>
        /// List of data sink plugins
        /// </summary>
        private List<IDataSink> DataSinkPlugins { get; set; }

        /// <summary>
        /// List of identified Tinkerforge sensors
        /// </summary>
        private List<TFSensor> TFSensorIdentified { get; }

        /// <summary>
        /// List of timers, for each data source one
        /// </summary>
        private List<System.Timers.Timer> DataSourceTimers { get; set; }

        /// <summary>
        /// Constructor of TF handler
        /// </summary>
        /// <param name="pluginPath">Path to plugins</param>
        /// <param name="tfHost">TF host</param>
        /// <param name="tfPort">TF port</param>
        internal PluginHandler(string pluginPath, string tfHost, int tfPort)
        {
            PluginPath = pluginPath;
            Host = tfHost;
            Port = tfPort;
            Connected = false;
            TFSensorIdentified = new List<TFSensor>();
            DataSourceTimers = new List<System.Timers.Timer>();
        }

        /// <summary>
        /// Establish connection to master brick
        /// </summary>
        /// <returns>true on succes, true on already connected, false on failure</returns>
        private bool Connect()
        {
            if (TFConnection != null || Connected)
            {
                return true;
            }

            TFConnection = new IPConnection();
            try
            {
                TFConnection.Connect(Host, Port);
                Connected = true;
            }
            catch (System.Net.Sockets.SocketException e)
            {
                System.Console.WriteLine($"{nameof(Connect)}: Connection Error [{e.Message}]");
            }

            if (Connected)
            {
                try
                {
                    TFConnection.EnumerateCallback -= IdentifySensors;
                    TFConnection.EnumerateCallback += IdentifySensors;
                    TFConnection.Enumerate();
                }
                catch (NotConnectedException e)
                {
                    System.Console.WriteLine($"{nameof(Connect)}: Enumeration Error [{e.Message}]");
                }
            }

            return Connected;
        }

        /// <summary>
        /// Callback for enumeration => List of sensors connected to master brick
        /// </summary>
        /// <param name="sender">IPConnection</param>
        /// <param name="UID">UID of brick/bricklet</param>
        /// <param name="connectedUID">UID of masterbrick</param>
        /// <param name="position">Position of brick/bricklet in stack</param>
        /// <param name="hardwareVersion">Info about hardware version</param>
        /// <param name="firmwareVersion">Info about firmware version</param>
        /// <param name="deviceIdentifier">Brick/Bricklet type identifier</param>
        /// <param name="enumerationType">Kind of enumeration</param>
        private void IdentifySensors(IPConnection sender, string UID, string connectedUID, char position,
                        short[] hardwareVersion, short[] firmwareVersion,
                        int deviceIdentifier, short enumerationType)
        {
            if (enumerationType == IPConnection.ENUMERATION_TYPE_CONNECTED
                || enumerationType == IPConnection.ENUMERATION_TYPE_AVAILABLE)
            {
                var currentSensor = new TFSensor();
                currentSensor.UID = UID;
                currentSensor.ConnectedUID = connectedUID;
                currentSensor.Position = position;
                currentSensor.HardwareVersion = string.Join(",", hardwareVersion.Select(x => x.ToString()).ToArray());
                currentSensor.FirmwareVersion = string.Join(",", firmwareVersion.Select(x => x.ToString()).ToArray());
                currentSensor.DeviceIdentifier = deviceIdentifier;
                currentSensor.EnumerationType = enumerationType;
                TFSensorIdentified.Add(currentSensor);
            }
        }

        /// <summary>
        /// Initialize data source plugins
        /// </summary>
        /// <returns>true on success, otherwise false</returns>
        private bool InitDataSourcePlugins()
        {
            if (!Connect())
            {
                return false;
            }

            DataSourcePlugins = PluginLoader<IDataSource>.TFPluginsLoad(PluginPath, DeviceConfig.Instance.PluginProductName);
            if (DataSourcePlugins.Count == 0)
            {
                System.Console.WriteLine($"{nameof(InitDataSourcePlugins)}: No sensor plugins found in [{PluginPath}].");
                return false;
            }

            foreach (var currentSensor in TFSensorIdentified)
            {
                var plugin = DataSourcePlugins.FirstOrDefault(p => currentSensor.DeviceIdentifier == p.SensorType);
                if (plugin == null)
                {
                    System.Console.WriteLine($"{nameof(InitDataSourcePlugins)}: No plugin found for sensor type [{currentSensor.DeviceIdentifier}].");
                    continue;
                }
                plugin.Init(TFConnection, currentSensor.UID);
                System.Console.WriteLine($"{nameof(InitDataSourcePlugins)}: Initialized [{plugin.Name}] plugin.");
            }
            return true;
        }

        /// <summary>
        /// Initialize datasink plugins
        /// </summary>
        /// <returns>true on success, otherwise false</returns>
        private bool InitDataSinkPlugins()
        {
            DataSinkPlugins = PluginLoader<IDataSink>.TFPluginsLoad(PluginPath, DeviceConfig.Instance.PluginProductName);
            if (DataSinkPlugins.Count == 0)
            {
                System.Console.WriteLine($"{nameof(InitDataSinkPlugins)}: No datasink plugins found in [{PluginPath}].");
                return false;
            }

            foreach (var currentPlugin in DataSinkPlugins)
            {
                try
                {
                    currentPlugin.IsInitialized = currentPlugin.Init();
                    System.Console.WriteLine($"{nameof(InitDataSinkPlugins)}: Initialized [{currentPlugin.Name}] plugin.");
                }
                catch (Exception e)
                {
                    System.Console.WriteLine($"{nameof(InitDataSinkPlugins)}: Cannot init plugin [{currentPlugin.GetType()}] => [{e.Message}]");
                    System.Console.WriteLine($"{nameof(InitDataSinkPlugins)}: Inner exception => [{e.InnerException}]");
                }
            }

            return true;
        }

        /// <summary>
        /// Initialize all plugins
        /// </summary>
        /// <returns>true on success, otherwise false</returns>
        internal bool Init()
        {
            bool InitDone = InitDataSourcePlugins();

            if (!InitDone)
            {
                return false;
            }

            return InitDataSinkPlugins();
        }

        /// <summary>
        /// Shutdown all datasink plugins
        /// </summary>
        internal void Shutdown()
        {
            foreach (var currentPlugin in DataSinkPlugins)
            {
                if (currentPlugin.IsInitialized)
                {
                    currentPlugin.Shutdown();
                }
            }
        }

        /// <summary>
        /// Method used in task
        /// </summary>
        /// <param name="plugin"></param>
        internal void PublishNewValues(object plugin)
        {
            if (plugin is IDataSource)
            {
                var valueList = (plugin as IDataSource).Values();
                foreach (var value in valueList)
                {
                    foreach (var currentPlugin in DataSinkPlugins)
                    {
                        if (currentPlugin.IsInitialized)
                        {
                            currentPlugin.HandleValue(value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Start for each data source plugin an own timer which starts a task
        /// </summary>
        internal void StartMeasurements()
        {
            foreach (var currentSensor in DataSourcePlugins)
            {
                System.Timers.Timer currentTimer = new System.Timers.Timer(currentSensor.ReadDelay);
                currentTimer.Elapsed += (o, args) =>
                {
                    var task = new Task(PublishNewValues, currentSensor);
                    task.Start();
                };
                currentTimer.Start();
                DataSourceTimers.Add(currentTimer);
            }
        }

        /// <summary>
        /// Wait until all running threads are stopped
        /// </summary>
        internal void StopMeasurements()
        {
            foreach (var currentTimer in DataSourceTimers)
            {
                currentTimer.Stop();
            }
        }
    }
}