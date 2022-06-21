using ArduinoLedController.Properties;
using Microsoft.Win32;
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
          
            

            string usbPort = tbSettingText.Text;
            Arduino arduino = new Arduino();
            arduino.UploadHexToArduino(usbPort);
            arduino.SendCommandForArduino(usbPort);
        }

        private void btnSetPin_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("pathHex: " + globalVariable.pathHex);
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true) {
                globalVariable.pathHex = openFileDialog.FileName;
            }
            MessageBox.Show("pathHex: " + globalVariable.pathHex);
        }
    }
}