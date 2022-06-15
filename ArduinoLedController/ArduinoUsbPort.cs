using System;
using System.IO.Ports;

namespace ArduinoLedController
{
    public class ArduinoUsbPort
    {
        SerialPort currentPort;
        public string GetCorrectArduinoUsbPort()
        {
            string result = null; // Correct Arduino Usb Port
            string[] ports = SerialPort.GetPortNames(); // All ports
            foreach (string port in ports)
            {
                currentPort = new SerialPort(port, 9600);
                if (IsCorrectArduinoUsbPort()) { result = port; }
            }
            if (result == null) { throw new NullReferenceException("USB port which Arduino is connected was not found"); }
            return result;
        }
        public bool IsCorrectArduinoUsbPort()
        {
            currentPort.Open();
            System.Threading.Thread.Sleep(1000);
            string returnMessage = currentPort.ReadLine();
            currentPort.Close();
            // in arduino sketch should be Serial.println("Info from Arduino") inside  void loop()
            return returnMessage.Contains("Info from Arduino") ? true : false;
        }
    }
}
