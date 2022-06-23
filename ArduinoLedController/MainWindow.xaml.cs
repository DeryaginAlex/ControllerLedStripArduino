using ArduinoLedController.Properties;
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
        public MainWindow()
        {
            InitializeComponent();
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
                Arduino arduino = new Arduino();
                arduino.UploadHexToArduino(port);
                arduino.SendCommandForArduino(port);
            }
        }

        private void btnPrintSketch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(String.Format("Path to the hex file: {0}", globalVariable.pathHex));
        }

        private void btnSetSketch_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                globalVariable.pathHex = openFileDialog.FileName;
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
    }
}