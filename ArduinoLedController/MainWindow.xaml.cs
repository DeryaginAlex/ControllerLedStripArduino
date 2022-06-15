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
        static int speed = 9600; //between 300 and 2'000'000
        static string pathHex = @"sketch_jun14a.ino.with_bootloader.standard.hex"; //path to the hex file
        private byte btnState = 0;
        public MainWindow()
        {
            InitializeComponent();
            string usbPort = arduinoUsbPort.GetCorrectArduinoUsbPort();
            // ArduinoUploader
            var uploader = new ArduinoSketchUploader(new ArduinoSketchUploaderOptions()
            {
                FileName = pathHex,
                PortName = usbPort,
                ArduinoModel = ArduinoModel.Micro //Model
            }
            );
            uploader.UploadSketch();
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            string usbPort = arduinoUsbPort.GetCorrectArduinoUsbPort();
            SerialPort serialPort = new SerialPort(usbPort, speed);
            if (btnState == 0)
            {
                serialPort.Open();
                serialPort.Write("1");
                serialPort.Close();
                btnState = 1;
                btn1.Content = "Red Led ON";
            }
            else
            {
                serialPort.Open();
                serialPort.Write("2");
                serialPort.Close();
                btnState = 0;
                btn1.Content = "Red Led OFF";
            }
        }
    }
}