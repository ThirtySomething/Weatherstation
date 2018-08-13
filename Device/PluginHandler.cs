using System;
using System.Collections.Generic;
using System.Linq;
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
        /// List of sensor plugins
        /// </summary>
        private List<IDataSource> SensorPlugins { get; set; }

        /// <summary>
        /// List of data sink plugins
        /// </summary>
        private List<IDataSink> DataSinkPlugins { get; set; }

        /// <summary>
        /// List of identified sensors
        /// </summary>
        private List<TFSensor> TFSensorIdentified { get; }

        /// <summary>
        /// Constructor of TF handler
        /// </summary>
        /// <param name="pluginPath">Path to plugins</param>
        /// <param name="tfHost">TF host</param>
        /// <param name="tfPort">TF port</param>
        internal PluginHandler(string pluginPath, string tfHost, int tfPort)
        {
            Host = tfHost;
            Port = tfPort;
            PluginPath = pluginPath;
            Connected = false;
            TFSensorIdentified = new List<TFSensor>();
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
                    TFConnection.EnumerateCallback -= IdentifySensorsCallBack;
                    TFConnection.EnumerateCallback += IdentifySensorsCallBack;
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
        private void IdentifySensorsCallBack(IPConnection sender, string UID, string connectedUID, char position,
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
        /// Initialize sensor plugins
        /// </summary>
        /// <returns>true on success, otherwise false</returns>
        private bool InitSensorPlugins()
        {
            if (!Connect())
            {
                return false;
            }

            SensorPlugins = PluginLoader<IDataSource>.TFPluginsLoad(PluginPath, ClientConfig.Instance.PluginProductName);
            if (SensorPlugins.Count == 0)
            {
                System.Console.WriteLine($"{nameof(InitSensorPlugins)}: No sensor plugins found in [{PluginPath}].");
                return false;
            }

            foreach (var currentSensor in TFSensorIdentified)
            {
                var plugin = SensorPlugins.FirstOrDefault(p => currentSensor.DeviceIdentifier == p.SensorType);
                if (plugin == null)
                {
                    System.Console.WriteLine($"{nameof(InitSensorPlugins)}: No plugin found for sensor type [{currentSensor.DeviceIdentifier}].");
                    continue;
                }
                plugin.Init(TFConnection, currentSensor.UID);
                System.Console.WriteLine($"{nameof(InitSensorPlugins)}: Initialized [{plugin.Name}] plugin.");
            }
            return true;
        }

        /// <summary>
        /// Initialize datasink plugins
        /// </summary>
        /// <returns>true on success, otherwise false</returns>
        private bool InitDataSinkPlugins()
        {
            DataSinkPlugins = PluginLoader<IDataSink>.TFPluginsLoad(PluginPath, ClientConfig.Instance.PluginProductName);
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
            bool InitDone = InitSensorPlugins();

            if (!InitDone)
            {
                return false;
            }

            return InitDataSinkPlugins();
        }

        /// <summary>
        /// Loop over all IDataSources, read data and return collection of all results
        /// </summary>
        /// <returns>Collection of (sensor type|sensor value)</returns>
        internal List<MeasurementValue> ValuesRead()
        {
            var pluginData = new List<MeasurementValue>();
            SensorPlugins.ForEach(p => pluginData.Add(p.Value()));
            return pluginData.OrderBy(p => p.SortOrder).ToList();
        }

        /// <summary>
        /// Feed each IDataSink plugin with sensor data
        /// </summary>
        /// <param name="SensorValues"></param>
        internal void HandleValues(List<MeasurementValue> SensorValues)
        {
            foreach (var currentPlugin in DataSinkPlugins)
            {
                if (currentPlugin.IsInitialized)
                {
                    foreach (var currentValue in SensorValues)
                    {
                        currentPlugin.HandleValue(currentValue);
                    }
                }
            }
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
    }
}