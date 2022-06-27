using System;
using System.IO.Ports;
using System.Windows;
using ArduinoUploader;
using ArduinoUploader.Hardware;
using Microsoft.Win32;
using System.Management;

namespace ArduinoLedController
{
    internal class Arduino
    {
        internal void InstallDrivers()
        {
            try
            {
                // installing drivers
                System.Diagnostics.Process.Start(GetGlobalVariable.PathArduinoDriver);
            }
            catch (Exception)
            {
                var result = MessageBox.Show("Driver File for arduino was not found.\n Do you want set path to driver file?", "Question", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        GetGlobalVariable.PathArduinoDriver = openFileDialog.FileName;
                        InstallDrivers();
                    }
                }
            }
        }
        internal ManagementObjectSearcher GetPorts()
        {
            ManagementScope connectionScope = new ManagementScope();
            SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
            ManagementObjectSearcher comPorts = new ManagementObjectSearcher(connectionScope, serialQuery);
            return comPorts;
        }
        internal string CheckAndGetValidPort(ManagementObjectSearcher comPorts)
        {
            string result = null;
            foreach (var comPort in comPorts.Get())
            {
                string description = comPort["Description"].ToString();
                string deviceId = comPort["DeviceID"].ToString();
                if (description.Contains("Arduino"))
                {
                    result = deviceId;
                }
            }
            if (result == null)
            {
                MessageBox.Show("Port not found");
            }
            return result;
        }

        internal void InstallCorrectPort(string comPorts) {
            GetGlobalVariable.VirtualComPort = comPorts;
            MessageBox.Show(String.Format("Arduino virtual COM-port ({0}) is found automatically", comPorts));
        }
        
        internal void UploadHexToArduino(string port)
        {
            try
            {
                // ArduinoUploader DLL
                var uploader = new ArduinoSketchUploader(new ArduinoSketchUploaderOptions()
                {
                    FileName = GetGlobalVariable.PathHex,
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
        internal void SendCommandForArduino(string port)
        {
            try
            {
                SerialPort serialPort = new SerialPort(port, GetGlobalVariable.Speed);
                serialPort.Open();      // connect to arduino
                serialPort.Write("1");  // set command for arduino
                serialPort.Close();     // disconnect from arduino
            }
            catch (Exception)
            {
                MessageBox.Show(String.Format("{0} does not exist", port));
            }
        }
    }
}