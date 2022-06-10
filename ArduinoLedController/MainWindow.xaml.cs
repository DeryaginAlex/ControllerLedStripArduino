using System.Windows;
using System.Windows.Media;
using System.IO.Ports; // for COM Ports

namespace ArduinoLedController
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string ComPort = "COM6";
        static int speed = 9600; //between 300 and 2'000'000

        SerialPort mySerialPort = new SerialPort(ComPort, speed);
        private byte btnstate = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (btnstate == 0)
            {
                mySerialPort.Open();
                mySerialPort.Write("1");
                mySerialPort.Close();
                btnstate = 1;
                btn1.Background = Brushes.Green;
                btn1.Content = "Red Led ON";
            }
            else
            {
                mySerialPort.Open();
                mySerialPort.Write("2");
                mySerialPort.Close();
                btnstate = 0;
                btn1.Background = Brushes.Red;
                btn1.Content = "Red Led OFF";
            }
        }
    }
}