using System.IO.Ports;

namespace ArduinoLedController
{
    public class ArduinoUsbPort
    {
        SerialPort currentPort;
        public string GetCorrectArduinoUsbPort()
        {
            string result = null;
            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                this.currentPort = new SerialPort(port, 9600);
                if (IsCorrectArduinoUsbPort())
                {
                    result = port;
                }
            }
            return result;
        }
        public bool IsCorrectArduinoUsbPort()
        {
            currentPort.Open();
            System.Threading.Thread.Sleep(1000);
            string returnMessage = currentPort.ReadLine();
            currentPort.Close();
            // in arduino sketch should be Serial.println("Info from Arduino") inside  void loop()
            if (returnMessage.Contains("Info from Arduino"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
