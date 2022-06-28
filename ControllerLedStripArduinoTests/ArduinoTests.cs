using System.Management;
using ControllerLedStripArduino;

namespace ControllerLedStripArduino;
public class ArduinoTests
{
    private Arduino testObjectArduino;

    [SetUp]
    public void Setup()
    {
        testObjectArduino = new Arduino();
    }
    [Test]
    public void Arduino_GetPorts_PortNotFound_Test()
    {
        bool result = false;
        try
        {
            var ports = testObjectArduino.GetPorts();
        }
        catch (Exception)
        {
            result = true;
        }
        Assert.IsTrue(result);
    }

    [Test]
    public void Arduino_CheckAndGetCorrectPort_PortsNotFound_Test()
    {
        var ports = new ManagementObjectSearcher(new ManagementScope(), new SelectQuery());
        bool result = false;

        try
        {
            var port = testObjectArduino.CheckAndGetCorrectPort(ports);
        }
        catch (FileNotFoundException)
        {
            result = true;
        }

        Assert.IsTrue(result);
    }


    [TestCase("COM6")]
    public void Arduino_InstallCorrectPort_PortsNotFound_Test(string port)
    {
        string expectation;

        GlobalVariable.VirtualComPort="COM8";
        expectation = GlobalVariable.VirtualComPort;
        // TODO Найти ошибку
        testObjectArduino.InstallCorrectPort(port);
        expectation = GlobalVariable.VirtualComPort;

        Assert.Equals(expectation, port);
    }
}
