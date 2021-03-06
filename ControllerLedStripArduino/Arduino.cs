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
    public class Arduino
    {
        public void InstallDrivers()
        {
            System.Diagnostics.Process.Start(GlobalVariable.PathArduinoDriver);
            try
            {
                // installing drivers
                System.Diagnostics.Process.Start(GlobalVariable.PathArduinoDriver);
            }
            catch (NullReferenceException)
            {
                var result = MessageBox.Show("Driver File for arduino was not found.\n Do you want set path to driver file?", "Question", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        GlobalVariable.PathArduinoDriver = openFileDialog.FileName;
                        InstallDrivers();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Generic Exception Handler: {e}");
                throw new Exception($"Generic Exception Handler: {e}");
            }
        }
        public ManagementObjectSearcher GetPorts()
        {
            try
            {
                ManagementScope connectionScope = new ManagementScope();
                SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
                ManagementObjectSearcher comPorts = new ManagementObjectSearcher(connectionScope, serialQuery);
                return comPorts;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Incorrect list of ports was received from the device manager");
                return new ManagementObjectSearcher(); // TO DO Delete this return. it is created only for the convenience of testing
                throw new Exception ("Incorrect list of ports was received from the device manager");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Generic Exception Handler: {e}");
                return new ManagementObjectSearcher();
                throw new Exception($"Generic Exception Handler: {e}");

            }
        }
        public string CheckAndGetCorrectPort(ManagementObjectSearcher ports)
        {
            try
            {
                string result = null;
                foreach (var comPort in ports.Get())
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
                throw new Exception("Port received from the device manager is incorrect");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Generic Exception Handler: {e}");
                return "COM9";
            }
        }
        public void InstallCorrectPort(string port)
        {
            try
            {
                GlobalVariable.VirtualComPort = port;
                MessageBox.Show(String.Format("Arduino virtual COM-port ({0}) is found automatically", port));
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Failed to install Com-port");
                throw new Exception("Failed to install Com-port");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Generic Exception Handler: {e}");
            }
        }
        public void UploadHexToArduino(string port)
        {
            try
            {
                // ArduinoUploader DLL
                var uploader = new ArduinoSketchUploader(new ArduinoSketchUploaderOptions()
                {
                    FileName = GlobalVariable.PathHex,
                    PortName = port,
                    ArduinoModel = ArduinoModel.Leonardo //Model Arduino
                });
                uploader.UploadSketch();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(String.Format("Usb port which Arduino is connected was not found.\n{0} is not correct port.", port));
                throw new Exception("Usb port which Arduino is connected was not found.Not correct port.");
            }
            catch (Exception e)
            {                
                MessageBox.Show($"Generic Exception Handler: {e}");
                throw new Exception($"Generic Exception Handler: {e}");
            }
        }
        public void SendCommandForArduino(string port)
        {
            try
            {
                SerialPort serialPort = new SerialPort(port, GlobalVariable.SpeedArduinoDataTransfer);
                serialPort.Open();      // connect to arduino
                serialPort.Write("1");  // set command for arduino
                serialPort.Close();     // disconnect from arduino
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(String.Format("Port does not exist"));
                throw new Exception("Port does not exist");
            }
            catch (Exception e)
            {
                MessageBox.Show($"Generic Exception Handler: {e}");
                throw new Exception($"Generic Exception Handler: {e}");
            }
        }
    }
}