using System;
using System.Collections.Generic;
using Tinkerforge;

namespace net.derpaul.tf.plugin
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
        private static BrickletLCD20x4 Bricklet { get; set; }

        /// <summary>
        /// Internal connection to TF master brick
        /// </summary>
        private IPConnection TFConnection { get; set; }

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
            Bricklet = null;
        }

        /// <summary>
        /// Cleanup Lcd
        /// </summary>
        public void Shutdown()
        {
            Bricklet.ClearDisplay();
            Bricklet.BacklightOff();
        }

        /// <summary>
        /// Part of interface TFDataSink - perform action with given data
        /// </summary>
        /// <param name="SensorValue"></param>
        public void HandleValue(MeasurementValue SensorValue)
        {
            if (Bricklet == null)
            {
                return;
            }

            // Display timestamp
            Bricklet.WriteLine(0, 0, SensorValue.Timestamp.ToString(LcdConfig.Instance.TimestampFormat));

            // Calculation of position in dependency of the sort order
            byte posX = (byte)((SensorValue.SortOrder % 2) * 10);
            byte posY = (byte)((SensorValue.SortOrder / 2) + 1);
            string MeasurementValueData = string.Format("{0,7:####.00} {1}", SensorValue.Value, SensorValue.Unit);

            Bricklet.WriteLine(posY, posX, MeasurementValueData);
        }

        /// <summary>
        /// Part of interface TFDataSink - initalize plugin
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            return PerformConnect() ? CollectBrickletInformations() : false;
        }

        /// <summary>
        /// Request list of connected bricklets
        /// </summary>
        /// <returns></returns>
        private bool CollectBrickletInformations()
        {
            try
            {
                TFConnection.EnumerateCallback -= InstantiateLCDBricklet;
                TFConnection.EnumerateCallback += InstantiateLCDBricklet;
                TFConnection.Enumerate();
            }
            catch (NotConnectedException e)
            {
                System.Console.WriteLine($"{nameof(CollectBrickletInformations)}: Enumeration Error [{e.Message}]");
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
            try
            {
                TFConnection = new IPConnection();
                TFConnection.Connect(LcdConfig.Instance.BrickDaemonIP, LcdConfig.Instance.BrickDaemonPort);
            }
            catch (System.Net.Sockets.SocketException e)
            {
                System.Console.WriteLine($"{nameof(PerformConnect)}: Connection Error [{e.Message}]");
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
            if ((enumerationType == IPConnection.ENUMERATION_TYPE_CONNECTED 
                || enumerationType == IPConnection.ENUMERATION_TYPE_AVAILABLE)
                && (deviceIdentifier == BrickletLCD20x4.DEVICE_IDENTIFIER))
            {
                Bricklet = new BrickletLCD20x4(UID, TFConnection);
                Bricklet.ClearDisplay();
                Bricklet.BacklightOn();
            }
        }
    }
}