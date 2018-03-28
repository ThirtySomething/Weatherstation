using System;
using System.Collections.Generic;
using Tinkerforge;

namespace net.derpaul.tf
{
    /// <summary>
    /// Class to write values to TF LCD20x4 bricklet
    /// </summary>
    public class Lcd : IDataSink
    {
        /// <summary>
        /// Get the name of class
        /// </summary>
        public string Name { get { return this.GetType().Name; } }

        /// <summary>
        /// Flags successful initialization
        /// </summary>
        public bool IsInitialized { get; set; } = false;

        /// <summary>
        /// Internal object of TF bricklet
        /// </summary>
        private static BrickletLCD20x4 _Bricklet { get; set; }

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
        public Lcd()
        {
            _TFSensorIdentified = new List<Tuple<int, string>>();
            _Bricklet = null;
        }

        /// <summary>
        /// Cleanup Lcd
        /// </summary>
        public void Shutdown()
        {
            _Bricklet.ClearDisplay();
            _Bricklet.BacklightOff();
        }

        /// <summary>
        /// Part of interface TFDataSink - perform action with given data
        /// </summary>
        /// <param name="SensorValue"></param>
        public void HandleValue(MeasurementValue SensorValue)
        {
            if (_Bricklet == null)
            {
                return;
            }

            // Display timestamp
            _Bricklet.WriteLine(0, 0, SensorValue.Timestamp.ToString(LcdConfig.Instance.TimestampFormat));

            // Calculation of position in dependency of the sort order
            byte posX = (byte)((SensorValue.SortOrder % 2) * 10);
            byte posY = (byte)((SensorValue.SortOrder / 2) + 1);
            string MeasurementValueData = string.Format("{0,7:####.00} {1}", SensorValue.Value, SensorValue.Unit);

            _Bricklet.WriteLine(posY, posX, MeasurementValueData);
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
                Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: Enumeration Error [{e.Message}]");
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
                _TFConnection.Connect(LcdConfig.Instance.Host, LcdConfig.Instance.Port);
            }
            catch (System.Net.Sockets.SocketException e)
            {
                Console.WriteLine($"{System.Reflection.MethodBase.GetCurrentMethod().Name}: Connection Error [{e.Message}]");
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
                _Bricklet = new BrickletLCD20x4(UID, _TFConnection);
                _Bricklet.ClearDisplay();
                _Bricklet.BacklightOn();
            }
        }
    }
}