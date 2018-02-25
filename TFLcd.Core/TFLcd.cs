using System;
using System.Collections.Generic;
using System.Linq;
using Tinkerforge;

namespace net.derpaul.tf
{
    /// <summary>
    /// Class to write values to LCD20x4 bricklet
    /// </summary>
    public class TFLcd : ITFDataSink
    {
        /// <summary>
        /// Host to connect to
        /// </summary>
        private string Host { get; } = "localhost";

        /// <summary>
        /// Port of ´host to connect to
        /// </summary>
        private int Port { get; } = 4223;

        /// <summary>
        /// Internal object of TF bricklet
        /// </summary>
        private static BrickletLCD20x4 _BrickletLCD20x4 { get; set; }

        /// <summary>
        /// Internal connection to TF master brick
        /// </summary>
        private IPConnection _TFConnection { get; set; }

        /// <summary>
        /// List of identified sensors
        /// </summary>
        private List<Tuple<int, string>> _TFSensorIdentified { get; }

        /// <summary>
        /// Constructor of plugin to initalize some internal fields
        /// </summary>
        public TFLcd()
        {
            _TFSensorIdentified = new List<Tuple<int, string>>();
            _BrickletLCD20x4 = null;
        }

        /// <summary>
        /// Part of interface TFDataSink - load config from file
        /// </summary>
        /// <returns>true on success, otherwise false</returns>
        public bool ConfigLoad()
        {
            // TODO: Load config from plugin specific XML file
            return true;
        }

        /// <summary>
        /// Part of interface TFDataSink - perform action with given data
        /// </summary>
        /// <param name="SensorValues"></param>
        public void HandleValues(ICollection<Tuple<string, double>> SensorValues)
        {
            if (_BrickletLCD20x4 == null)
            {
                return;
            }

            foreach (var currentDataPair in SensorValues)
            {
                string text;
                switch (currentDataPair.Item1)
                {
                    case "TFAmbientLight":
                        text = string.Format("Illuminanc {0,6:###.00} lx", currentDataPair.Item2);
                        _BrickletLCD20x4.WriteLine(0, 0, text);
                        break;

                    case "TFHumidity":
                        text = string.Format("Humidity   {0,6:###.00} %", currentDataPair.Item2);
                        _BrickletLCD20x4.WriteLine(1, 0, text);
                        break;

                    case "TFAirPressure":
                        text = string.Format("Air Press {0,7:####.00} mb", currentDataPair.Item2);
                        _BrickletLCD20x4.WriteLine(2, 0, text);
                        break;

                    case "TFTemperature":
                        text = string.Format("Temperature {0,5:##.00} {1}C", currentDataPair.Item2, (char)0xDF);
                        _BrickletLCD20x4.WriteLine(3, 0, text);
                        break;
                }
            }
        }

        /// <summary>
        /// Part of interface TFDataSink - initalize plugin
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            var Success = PerformConnect();
            if (!Success)
            {
                return false;
            }

            return CollectBrickletInformations();
        }

        /// <summary>
        /// Request list of connected bricklets
        /// </summary>
        /// <returns></returns>
        private bool CollectBrickletInformations()
        {
            try
            {
                _TFConnection.EnumerateCallback -= InstantiateLCDBricklet;
                _TFConnection.EnumerateCallback += InstantiateLCDBricklet;
                _TFConnection.Enumerate();
            }
            catch (NotConnectedException e)
            {
                System.Console.WriteLine($"Enumeration Error [{e.Message}]");
                System.Threading.Thread.Sleep(Defines.constDefaultDelay);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Connect to tinkerforge daemon
        /// </summary>
        /// <returns></returns>
        private bool PerformConnect()
        {
            _TFConnection = new IPConnection();
            try
            {
                _TFConnection.Connect(Host, Port);
            }
            catch (System.Net.Sockets.SocketException e)
            {
                System.Console.WriteLine($"Connection Error [{e.Message}]");
                System.Threading.Thread.Sleep(Defines.constDefaultDelay);
                return false;
            }
            return true;
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
        private void InstantiateLCDBricklet(IPConnection sender, string UID, string connectedUID, char position,
                short[] hardwareVersion, short[] firmwareVersion,
                int deviceIdentifier, short enumerationType)
        {
            if ((enumerationType == IPConnection.ENUMERATION_TYPE_CONNECTED ||
               enumerationType == IPConnection.ENUMERATION_TYPE_AVAILABLE) &&
               (deviceIdentifier == BrickletLCD20x4.DEVICE_IDENTIFIER))
            {
                _BrickletLCD20x4 = new BrickletLCD20x4(UID, _TFConnection);
                _BrickletLCD20x4.ClearDisplay();
                _BrickletLCD20x4.BacklightOn();
                System.Console.WriteLine($"Datasink of type [{GetType().Name}] instantiated.");
            }
        }
    }
}