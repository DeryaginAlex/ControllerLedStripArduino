using Microsoft.Win32;
using System;
using System.Windows;

namespace ArduinoLedController
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Arduino arduino = new Arduino();
        public MainWindow()
        {
            InitializeComponent();

            var ports = arduino.GetPorts();
            var port = arduino.CheckAndGetCorrectPort(ports);
            arduino.InstallCorrectPort(port);
        }

            private void btnExceptionGeneration_Click(object sender, RoutedEventArgs e)
        {
            string port = tbSettingText.Text;
            if (string.IsNullOrEmpty(port))
            {
                MessageBox.Show("Port not entered");
            }
            else
            {
                arduino.UploadHexToArduino(port);
                arduino.SendCommandForArduino(port);
            }
        }

        private void btnPrintSketch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(String.Format("Path to the hex file: {0}", GetGlobalVariable.PathHex));
        }

        private void btnSetSketch_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                GetGlobalVariable.PathHex = openFileDialog.FileName;
            }
        }

        private void checkBoxComPort_Checked(object sender, RoutedEventArgs e)
        {
            tbSettingText.IsReadOnly = true;
        }

        private void checkBoxComPort_Unchecked(object sender, RoutedEventArgs e)
        {
            tbSettingText.IsReadOnly = false;
        }

        private void btnInstallingDriver_Click(object sender, RoutedEventArgs e)
        {
            arduino.InstallDrivers();
        }

        private void btnSetVirtualComPort_Click(object sender, RoutedEventArgs e)
        {
            GetGlobalVariable.VirtualComPort = tbSettingText.Text;
            if (string.IsNullOrEmpty(GetGlobalVariable.VirtualComPort))
            {
                MessageBox.Show("Port not entered");
            }
        }
    }
}