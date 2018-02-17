using System;
using System.Collections.Generic;
using System.Linq;
using Tinkerforge;

namespace net.derpaul.tf
{
    /// <summary>
    /// Class to deal with collection of TF sensors, addressed by a common interface
    /// </summary>
    internal class TFHandler
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
        /// List of plugins
        /// </summary>
        private ICollection<ITFSensor> _Plugins { get; set; }

        /// <summary>
        /// List of identified sensors
        /// </summary>
        private List<Tuple<int, string>> _TFSensorIdentified { get; }

        /// <summary>
        /// Constructor of TF handler
        /// </summary>
        /// <param name="tfHost">TF host</param>
        /// <param name="tfPort">TF port</param>
        internal TFHandler(string pluginPath, string tfHost, int tfPort)
        {
            _Host = tfHost;
            _Port = tfPort;
            _PluginPath = pluginPath;
            _Connected = false;
            _TFSensorIdentified = new List<Tuple<int, string>>();
        }

        /// <summary>
        /// Establish connection to master brick
        /// </summary>
        /// <returns>true on succes, true on already connected, false on failure</returns>
        internal bool Connect()
        {
            if (_TFConnection != null || _Connected == true)
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
                System.Console.WriteLine($"Connection Error [{e.Message}]");
                System.Threading.Thread.Sleep(Defines.constDefaultDelay);
            }

            if (_Connected)
            {
                try
                {
                    _TFConnection.EnumerateCallback += IdentifySensorsCallBack;
                    _TFConnection.Enumerate();
                    // _TFConnection.EnumerateCallback -= IdentifySensorsCallBack;
                }
                catch (NotConnectedException e)
                {
                    System.Console.WriteLine($"Enumeration Error [{e.Message}]");
                    System.Threading.Thread.Sleep(Defines.constDefaultDelay);
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
                var hVersion = string.Join(",", hardwareVersion.Select(x => x.ToString()).ToArray());
                var fVersion = string.Join(",", firmwareVersion.Select(x => x.ToString()).ToArray());
                System.Console.WriteLine($"Sensor [{UID}], type [{deviceIdentifier}], hardware [{hVersion}], firmware [{fVersion}].");
                _TFSensorIdentified.Add(new Tuple<int, string>(deviceIdentifier, UID));
            }
        }

        /// <summary>
        /// Initialize all plugins
        /// </summary>
        internal void Init()
        {
            _Plugins = TFPluginLoader<ITFSensor>.TFPluginsLoad(_PluginPath);
            // TODO: Replace foreach loop with a Linq statement
            foreach (var currentSensor in _TFSensorIdentified)
            {
                var plugin = _Plugins.FirstOrDefault(p => currentSensor.Item1 == p.SensorType);
                if (plugin == null)
                {
                    System.Console.WriteLine($"No plugin found for sensor type [{currentSensor.Item1}].");
                    continue;
                }
                plugin.Init(_TFConnection, currentSensor.Item2);
            }
        }

        /// <summary>
        /// Loop over all sensors, read value name and type, return collection of all results
        /// </summary>
        /// <returns>Collection of (sensor type|sensor value)</returns>
        internal ICollection<Tuple<string, double>> ValuesRead()
        {
            ICollection<Tuple<string, double>> pluginData = new List<Tuple<string, double>>();
            // TODO: Replace foreach loop with a Linq statement
            foreach (ITFSensor currentPlugin in _Plugins)
            {
                string type = currentPlugin.Name;

                double value = currentPlugin.ValueGet();
                pluginData.Add(new Tuple<string, double>(type, value));
            }

            return pluginData;
        }
    }
}