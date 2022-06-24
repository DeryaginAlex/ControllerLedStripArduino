namespace ArduinoLedController
{
    internal static class GetGlobalVariable
    {
        public static int speed = 9600; //between 300 and 2'000'000
        public static string pathHex = System.IO.Path.GetFullPath(@"sketch.hex"); //path to hex file
        public static string pathArduinoDriver = System.IO.Path.GetFullPath(@"drivers/dpinst-amd64.exe"); //path to Arduino Driver
    }
}