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

    //method InstallDrivers tested through property GlobalVariable.PathArduinoDriver

    [Test]
    public void Arduino_GetPorts_ListOfPortsIsEmpty_Test()
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

    [TestCase(null)]
    public void Arduino_CheckAndGetCorrectPort_PortsNotFound_Test(ManagementObjectSearcher ports)
    {
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
    
    [TestCase(null)]
    public void Arduino_InstallCorrectPort_IncorrectInput_Test(string port)
    {
        bool result = false;

        try
        {
            testObjectArduino.InstallCorrectPort(port);
        }
        catch (Exception)
        {
            result = true;
        }

        Assert.IsTrue(result);
    }

    [TestCase(null)]
    public void Arduino_UploadHexToArduino_IncorrectInput_Test(string port)
    {
        bool result = false;

        try
        {
            testObjectArduino.UploadHexToArduino(port);
        }
        catch (FileNotFoundException)
        {
            result = true;
        }

        Assert.IsTrue(result);
    }

    [TestCase(null)]
    public void Arduino_SendCommandForArduino_PortNotFound_Test(string port) {
        bool result = false;

        try
        {
            testObjectArduino.SendCommandForArduino(port);
        }
        catch (FileNotFoundException)
        {
            result = true;
        }

        Assert.IsTrue(result);
    }

}
