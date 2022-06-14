using System.Windows;
using System.IO.Ports; // for COM Ports
using ArduinoUploader;
using ArduinoUploader.Hardware;

namespace ArduinoLedController
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int speed = 9600; //between 300 and 2'000'000
        static string pathHex = @"sketch_jun14a.ino.with_bootloader.standard.hex"; //path to the hex file
        //private delegate void updateDelegate(string txt);
        private byte btnstate = 0;

        public MainWindow()
        {
            InitializeComponent();
            ArduinoUsbPort arduinoUsbPort = new ArduinoUsbPort();
            string usbPort = arduinoUsbPort.GetCorrectArduinoUsbPort();
            var uploader = new ArduinoSketchUploader(new ArduinoSketchUploaderOptions()
            {
                FileName = pathHex,
                PortName = usbPort,
                ArduinoModel = ArduinoModel.UnoR3 //Model
            }
            );
            uploader.UploadSketch();

            

        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            ArduinoUsbPort arduinoUsbPort = new ArduinoUsbPort();
            string usbPort = arduinoUsbPort.GetCorrectArduinoUsbPort();
            SerialPort serialPort = new SerialPort(usbPort, speed);
            if (btnstate == 0)
            {
                serialPort.Open();
                serialPort.Write("1");
                serialPort.Close();
                btnstate = 1;
                btn1.Content = "Red Led ON";
            }
            else
            {
                serialPort.Open();
                serialPort.Write("2");
                serialPort.Close();
                btnstate = 0;
                btn1.Content = "Red Led OFF";
            }
        }
    }
}