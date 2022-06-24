using System;
using System.IO.Ports;
using System.Windows;
using ArduinoUploader;
using ArduinoUploader.Hardware;
using Microsoft.Win32;

namespace ArduinoLedController
{
    internal class Arduino
    {
        internal void SendCommandForArduino(string port)
        {
            try
            {
                SerialPort serialPort = new SerialPort(port, GetGlobalVariable.speed);
                serialPort.Open();      // connect to arduino
                serialPort.Write("1");  // set command for arduino
                serialPort.Close();     // disconnect from arduino
            }
            catch (Exception)
            {
                MessageBox.Show(String.Format("{0} does not exist", port));
            }
        }

        internal void UploadHexToArduino(string port)
        {
            try
            {
                // ArduinoUploader DLL
                var uploader = new ArduinoSketchUploader(new ArduinoSketchUploaderOptions()
                {
                    FileName = GetGlobalVariable.pathHex,
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

        internal void InstallingDriver()
        {
            try
            {
                // installing drivers
                System.Diagnostics.Process.Start(GetGlobalVariable.pathArduinoDriver);
            }
            catch (Exception)
            {
                if (MessageBox.Show("Driver File for arduino was not found.\n" +
                    "Do you want set path to driver file?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        GetGlobalVariable.pathArduinoDriver = openFileDialog.FileName;
                        InstallingDriver();
                    }
                }
            }
        }
    }
}