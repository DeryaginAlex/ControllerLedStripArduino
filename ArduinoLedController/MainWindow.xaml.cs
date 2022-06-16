using System.Windows;

namespace ArduinoLedController
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
            private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Arduino arduino = new Arduino();
            string usbPort = arduino.GetCorrectArduinoUsbPort();
            arduino.UploadHexToArduino(usbPort);
            arduino.SendCommandForArduino(usbPort);
        }
    }
}