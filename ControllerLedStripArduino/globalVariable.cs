using System;

namespace ControllerLedStripArduino
{
    public static class GlobalVariable
    {
        #region private fields of class
        private static int speedArduinoDataTransfer = 9600; //between 300 and 2'000'000
        private static string pathHex = System.IO.Path.GetFullPath(@"sketchs/sketch.hex"); //path to hex file
        private static string pathArduinoDriver = System.IO.Path.GetFullPath(@"drivers/dpinst-amd64.exe"); //path to Arduino Driver
        private static string virtualComPort = "COM1";
        #endregion

        #region public properties of class
        public static int SpeedArduinoDataTransfer
        {
            get
            {
                if (speedArduinoDataTransfer < 300 && speedArduinoDataTransfer > 2000000)
                {
                    throw new IndexOutOfRangeException("speedArduinoDataTransfer - must be between 300 and 2'000'000");
                }
                else { return speedArduinoDataTransfer; }
            }
            set { speedArduinoDataTransfer = value; }
        }
        public static string VirtualComPort
        {
            get
            {
                if (string.IsNullOrEmpty(virtualComPort) && string.IsNullOrWhiteSpace(virtualComPort))
                {
                    throw new ArgumentNullException("virtualComPort not entered");
                }
                else { return virtualComPort; }
            }
            set { virtualComPort = value; }
        }
        public static string PathArduinoDriver
        {
            get
            {
                if (string.IsNullOrEmpty(pathArduinoDriver) && string.IsNullOrWhiteSpace(pathArduinoDriver))
                {
                    throw new NullReferenceException("Path Arduino Driver not entered");
                }
                else { return pathArduinoDriver; }
            }
            set { pathArduinoDriver = value; }
        }
        public static string PathHex
        {
            get
            {
                if (string.IsNullOrEmpty(pathHex) && string.IsNullOrWhiteSpace(pathHex))
                {
                    throw new NullReferenceException("Path HEX File not entered");
                }
                else { return pathHex; }
            }
            set { pathHex = value; }
        }
        #endregion
    }
}