using System.Management;

namespace ControllerLedStripArduino;
public class Tests
{
    private Arduino testObject;
    [SetUp]
    public void Setup()
    {
        testObject = new Arduino();
    }
    [Test]
    public void Arduino_GetPorts_PortNotFound_Test()
    {
        bool result = false;
        try
        {
            var ports = testObject.GetPorts();
        }
        catch (Exception)
        {
            result = true;
        }
        Assert.IsTrue(result);
    }

    [TestCase()]
    public void Arduino_CheckAndGetCorrectPort_PortsNotFound_Test()
    {
        ManagementScope scope = new ManagementScope();
        SelectQuery query = new SelectQuery();
        var ports = new ManagementObjectSearcher(scope, query);
        bool result = false;

        try
        {
            var port = testObject.CheckAndGetCorrectPort(ports);
        }
        catch (FileNotFoundException)
        {
            result = true;
        }
        Assert.IsTrue(result);
    }

}
