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
    
    [Test]
    public void Arduino_InstallCorrectPort_PortsNotFound_Test()
    {
        bool result = false;

        try
        {
            testObjectArduino.InstallCorrectPort(null);
        }
        catch (Exception)
        {
            result = true;
        }

        Assert.IsTrue(result);
    }

    [Test]
    public void Arduino_SendCommandForArduino_PortIsNull_Test() {
        bool result = false;

        try
        {
            testObjectArduino.SendCommandForArduino(null);
        }
        catch (FileNotFoundException)
        {
            result = true;
        }

        Assert.IsTrue(result);
    }

    [Test]
    public void Arduino_UploadHexToArduino_IncorrectInput_Test()
    {
        bool result = false;

        try
        {
            testObjectArduino.UploadHexToArduino(null);
        }
        catch (FileNotFoundException)
        {
            result = true;
        }

        Assert.IsTrue(result);
    }

}
