using System.Windows;
using System.IO.Ports;
using ArduinoUploader;
using ArduinoUploader.Hardware;

namespace ArduinoLedController
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ArduinoUsbPort arduinoUsbPort = new ArduinoUsbPort();
        private static int speed = 9600; //between 300 and 2'000'000
        private static string pathHex = @"..\..\..\..\sketch.hex"; //path to the hex file
        private static byte btnState = 0;
        public MainWindow()
        {
            InitializeComponent();
            string usbPort = arduinoUsbPort.GetCorrectArduinoUsbPort();
            // ArduinoUploader DLL
            var uploader = new ArduinoSketchUploader(new ArduinoSketchUploaderOptions()
            {
                FileName = pathHex,
                PortName = usbPort,
                ArduinoModel = ArduinoModel.Leonardo //Model Arduino
            });
            uploader.UploadSketch();    // loading hex to Arduino
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            string usbPort = arduinoUsbPort.GetCorrectArduinoUsbPort(); 
            SerialPort serialPort = new SerialPort(usbPort, speed); // connect to Arduino Usb Port
            if (btnState == 0)
            {
                serialPort.Open();      // connect to arduino
                serialPort.Write("1");  // set command for arduino
                serialPort.Close();     // disconnect from arduino
                btnState = 1;           // change state
                btn1.Content = "Red Led ON";
            }
            else
            {
                serialPort.Open();
                serialPort.Write("0");
                serialPort.Close();
                btnState = 0;
                btn1.Content = "Red Led OFF";
            }
        }
    }
}