using System;
using System.IO;
using System.IO.Ports;
using System.Windows;
using ArduinoUploader;
using ArduinoUploader.Hardware;
using Microsoft.Win32;
using System.Management;

namespace ControllerLedStripArduino
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
            catch (FileNotFoundException)
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
            catch (Exception e)
            {
                MessageBox.Show($"Generic Exception Handler: {e}");
            }

        }
        internal ManagementObjectSearcher GetPorts()
        {
            try
            {
                ManagementScope connectionScope = new ManagementScope();
                SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
                ManagementObjectSearcher comPorts = new ManagementObjectSearcher(connectionScope, serialQuery);
                return comPorts;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Incorrect list of ports was received from the device manager");
                return new ManagementObjectSearcher(); // TO DO Delete this return. it is created only for the convenience of testing
                //throw new Exception ("Incorrect list of ports was received from the device manager");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Generic Exception Handler: {e}");
                return new ManagementObjectSearcher();
            }
        }
        internal string CheckAndGetCorrectPort(ManagementObjectSearcher comPorts)
        {
            try
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
                    MessageBox.Show("Port is null");
                    throw new ArgumentNullException("Port not found");
                }
                return result;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Port received from the device manager is incorrect");
                return "COM9"; // TO DO Delete this return. it is created only for the convenience of testing
                //throw new Exception("Port received from the device manager is incorrect");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Generic Exception Handler: {e}");
                return "COM9";
            }
        }
        internal void InstallCorrectPort(string comPort)
        {
            try
            {
                GetGlobalVariable.VirtualComPort = comPort;
                MessageBox.Show(String.Format("Arduino virtual COM-port ({0}) is found automatically", comPort));
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Failed to install Com-port");
                //throw new Exception("Failed to install Com-port");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Generic Exception Handler: {e}");
            }
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
                //throw new Exception("Usb port which Arduino is connected was not found.Not correct port.");
            }
            //catch (Exception e)
            //{
            //    MessageBox.Show($"Generic Exception Handler: {e}");
            //}
        }
        internal void SendCommandForArduino(string port)
        {
            try
            {
                SerialPort serialPort = new SerialPort(port, GetGlobalVariable.SpeedArduinoDataTransfer);
                serialPort.Open();      // connect to arduino
                serialPort.Write("1");  // set command for arduino
                serialPort.Close();     // disconnect from arduino
            }
            catch (Exception)
            {
                MessageBox.Show(String.Format("{0} does not exist", port));
                //throw new Exception("Port does not exist");
            }
        }
    }
}