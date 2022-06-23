using System;
using System.IO.Ports;
using System.Windows;
using ArduinoUploader;
using ArduinoUploader.Hardware;

namespace ArduinoLedController
{
    internal class Arduino
    {
        public void SendCommandForArduino(string port)
        {            
            try
            {
                SerialPort serialPort = new SerialPort(port, globalVariable.speed);
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
            try
            {
                // ArduinoUploader DLL
                var uploader = new ArduinoSketchUploader(new ArduinoSketchUploaderOptions()
                {
                    FileName = globalVariable.pathHex,
                    PortName = port,
                    ArduinoModel = ArduinoModel.Leonardo //Model Arduino
                });
                uploader.UploadSketch();
            }
            catch (Exception)
            {
                MessageBox.Show(String.Format("Usb port which Arduino is connected was not found.\n{0} is not correct port.", port));                
            }
        }
    }
}