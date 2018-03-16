using System;
using System.Collections.Generic;
using System.Linq;
using Tinkerforge;

namespace net.derpaul.tf
{
    /// <summary>
    /// Class to deal with collection of TF sensors, addressed by a common interface
    /// </summary>
    internal class PluginHandler
    {
        /// <summary>
        /// TF host
        /// </summary>
        private string _Host { get; set; }

        /// <summary>
        /// TF port
        /// </summary>
        private int _Port { get; set; }

        /// <summary>
        /// The path to the plugins
        /// </summary>
        private string _PluginPath { get; set; }

        /// <summary>
        /// Internal connection to TF master brick
        /// </summary>
        private IPConnection _TFConnection { get; set; }

        /// <summary>
        /// Internal flag for successful connection
        /// </summary>
        private bool _Connected { get; set; }

        /// <summary>
        /// List of sensor plugins
        /// </summary>
        private List<ISensor> _SensorPlugins { get; set; }

        /// <summary>
        /// List of data sink plugins
        /// </summary>
        private List<IDataSink> _DataSinkPlugins { get; set; }

        /// <summary>
        /// List of identified sensors
        /// </summary>
        private List<TFSensor> _TFSensorIdentified { get; }

        /// <summary>
        /// Constructor of TF handler
        /// </summary>
        /// <param name="pluginPath">Path to plugins</param>
        /// <param name="tfHost">TF host</param>
        /// <param name="tfPort">TF port</param>
        internal PluginHandler(string pluginPath, string tfHost, int tfPort)
        {
            _Host = tfHost;
            _Port = tfPort;
            _PluginPath = pluginPath;
            _Connected = false;
            _TFSensorIdentified = new List<TFSensor>();
        }

        /// <summary>
        /// Establish connection to master brick
        /// </summary>
        /// <returns>true on succes, true on already connected, false on failure</returns>
        private bool Connect()
        {
            if (_TFConnection != null || _Connected)
            {
                return _Connected;
            }

            _TFConnection = new IPConnection();
            try
            {
                _TFConnection.Connect(_Host, _Port);
                _Connected = true;
            }
            catch (System.Net.Sockets.SocketException e)
            {
                Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: Connection Error [{e.Message}]");
            }

            if (_Connected)
            {
                try
                {
                    _TFConnection.EnumerateCallback -= IdentifySensorsCallBack;
                    _TFConnection.EnumerateCallback += IdentifySensorsCallBack;
                    _TFConnection.Enumerate();
                }
                catch (NotConnectedException e)
                {
                    Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: Enumeration Error [{e.Message}]");
                }
            }

            return _Connected;
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
            if (enumerationType == IPConnection.ENUMERATION_TYPE_CONNECTED ||
               enumerationType == IPConnection.ENUMERATION_TYPE_AVAILABLE)
            {
                var currentSensor = new TFSensor();
                currentSensor.UID = UID;
                currentSensor.ConnectedUID = connectedUID;
                currentSensor.Position = position;
                currentSensor.HardwareVersion = string.Join(",", hardwareVersion.Select(x => x.ToString()).ToArray());
                currentSensor.FirmwareVersion = string.Join(",", firmwareVersion.Select(x => x.ToString()).ToArray());
                currentSensor.DeviceIdentifier = deviceIdentifier;
                currentSensor.EnumerationType = enumerationType;
                _TFSensorIdentified.Add(currentSensor);
            }
        }

        /// <summary>
        /// Initialize sensor plugins
        /// </summary>
        /// <returns>true on success, otherwise false</returns>
        private bool InitSensorPlugins()
        {
            if (Connect() == false)
            {
                return false;
            }
            _SensorPlugins = PluginLoader<ISensor>.TFPluginsLoad(_PluginPath);

            if (_SensorPlugins.Count == 0)
            {
                Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: No sensor plugins found in [{_PluginPath}].");
                return false;
            }

            foreach (var currentSensor in _TFSensorIdentified)
            {
                var plugin = _SensorPlugins.FirstOrDefault(p => currentSensor.DeviceIdentifier == p.SensorType);
                if (plugin == null)
                {
                    Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: No plugin found for sensor type [{currentSensor.DeviceIdentifier}].");
                    continue;
                }
                plugin.Init(_TFConnection, currentSensor.UID);
            }
            return true;
        }

        /// <summary>
        /// Initialize datasink plugins
        /// </summary>
        /// <returns>true on success, otherwise false</returns>
        private bool InitDataSinkPlugins()
        {
            _DataSinkPlugins = PluginLoader<IDataSink>.TFPluginsLoad(_PluginPath);
            if (_DataSinkPlugins.Count == 0)
            {
                Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: No datasink plugins found in [{_PluginPath}].");
                return false;
            }

            foreach (var currentPlugin in _DataSinkPlugins)
            {
                try
                {
                    currentPlugin.Init();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: Cannot init plugin[{currentPlugin.GetType()}] => [{e.Message}]");
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
                return InitDone;
            }

            return InitDataSinkPlugins();
        }

        /// <summary>
        /// Loop over all sensors, read value name and type, return collection of all results
        /// </summary>
        /// <returns>Collection of (sensor type|sensor value)</returns>
        internal List<MeasurementValue> ValuesRead()
        {
            var pluginData = new List<MeasurementValue>();
            foreach (var currentPlugin in _SensorPlugins)
            {
                pluginData.Add(currentPlugin.ValueGet());
            }

            return pluginData.OrderBy(o => o.SortOrder).ToList();
        }

        /// <summary>
        /// Feed each datasink plugin with sensor data
        /// </summary>
        /// <param name="SensorValues"></param>
        internal void HandleValues(List<MeasurementValue> SensorValues)
        {
            foreach (var currentPlugin in _DataSinkPlugins)
            {
                if (!currentPlugin.IsInitialized)
                {
                    continue;
                }

                currentPlugin.HandleValues(SensorValues);
            }
        }

        /// <summary>
        /// Shutdown all datasink plugins
        /// </summary>
        internal void Shutdown()
        {
            foreach (var currentPlugin in _DataSinkPlugins)
            {
                if (!currentPlugin.IsInitialized)
                {
                    continue;
                }

                currentPlugin.Shutdown();
            }
        }
    }
}