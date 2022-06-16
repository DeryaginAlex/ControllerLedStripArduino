using System;
using System.IO.Ports;
using System.Windows;
using ArduinoUploader;
using ArduinoUploader.Hardware;

namespace ArduinoLedController
{
    internal class Arduino
    {
        private static int speed = 9600; //between 300 and 2'000'000
        private static string pathHex = @"sketch.hex"; //path to the hex file
        public void SendCommandForArduino(string port)
        {
            SerialPort serialPort = new SerialPort(port, speed);
            try
            {
                serialPort.Open();      // connect to arduino
                serialPort.Write("1");  // set command for arduino
                serialPort.Close();     // disconnect from arduino
            }
            catch (Exception)
            {
                MessageBox.Show(String.Format("{0} does not exist", port));
            }

        }

        public void UploadHexToArduino(string port)
        {
            // ArduinoUploader DLL
            var uploader = new ArduinoSketchUploader(new ArduinoSketchUploaderOptions()
            {
                FileName = pathHex,
                PortName = port,
                ArduinoModel = ArduinoModel.Leonardo //Model Arduino
            });

            try
            {
                uploader.UploadSketch();
            }
            catch
            {
                //String.Format("The current price is {0} per ounce.",pricePerOunce);
                MessageBox.Show(String.Format("USB port which Arduino is connected was not found.\n{0} is not correct port.", port));
            }
        }
        public string GetCorrectArduinoUsbPort()
        {
            string result = null; // Correct Arduino Usb Port
            string[] ports = SerialPort.GetPortNames(); // All ports
            SerialPort currentPort;
            foreach (string port in ports)
            {
                currentPort = new SerialPort(port, speed);
                currentPort.Open();
                System.Threading.Thread.Sleep(1000);
                string returnMessage = currentPort.ReadLine();
                currentPort.Close();
                if (returnMessage == "Info from Arduino") { result = port; }
            }
            if (result == null)
            {
                MessageBox.Show("value port cannot be null");
                //throw new NullReferenceException("USB port which Arduino is connected was not found");
            }
            return result;
        }
    }
}