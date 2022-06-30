using System;

namespace ControllerLedStripArduino
{
    public static class GlobalVariable
    {
        #region private fields of class

        // When connecting the arduino to computer via USB
        // Virtual COM port is being created (Not USB)
        private static int speedArduinoDataTransfer = 9600; //between 300 and 2'000'000
        private static string pathHex = System.IO.Path.GetFullPath(@"sketchs/sketch.hex"); //path to hex file
        private static string pathArduinoDriver = System.IO.Path.GetFullPath(@"drivers/dpinst-amd64.exe"); //path to Arduino Driver
        private static string virtualComPort = "COM1";
        #endregion

        #region public properties of class
        public static int SpeedArduinoDataTransfer
        {
            get { return speedArduinoDataTransfer; }
            set
            {
                if (value < 300 || value > 2000000)
                {
                    throw new IndexOutOfRangeException("speedArduinoDataTransfer - must be between 300 and 2'000'000");
                }
                else
                {
                    speedArduinoDataTransfer = value;
                }

            }
        }
        public static string VirtualComPort
        {
            get { return virtualComPort; }
            set
            {
                if (string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("virtualComPort not entered");
                }
                else
                {
                    virtualComPort = value;
                }
            }
        }
        public static string PathArduinoDriver
        {
            get
            {
                return pathArduinoDriver;
            }
            set
            {
                if (string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("Path Arduino Driver not entered");
                }
                else
                {
                    pathArduinoDriver = value;
                }
            }
        }
        public static string PathHex
        {
            get
            {
                return pathHex;
            }
            set
            {
                if (string.IsNullOrEmpty(value) && string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("Path HEX File not entered");
                }
                else
                {
                    pathHex = value;
                }
            }
        }
        #endregion
    }
}