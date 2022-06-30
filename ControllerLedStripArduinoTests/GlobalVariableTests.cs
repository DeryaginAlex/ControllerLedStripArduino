using System.Management;
using ControllerLedStripArduino;

namespace ControllerLedStripArduino;
public class GlobalVariableTests
{
    [SetUp]
    public void Setup(){}

    #region SpeedArduinoDataTransfer

    [TestCase(0)]
    [TestCase(100000000)]
    public void GlobalVariable_SpeedArduinoDataTransfer_OutOfRange_Test(int speed)
    {
        bool result = false;
        
        try
        {
            GlobalVariable.SpeedArduinoDataTransfer = speed;
        }
        catch (Exception)
        {
            result = true;
        }

        Assert.IsTrue(result);
    }

    #endregion

    #region VirtualComPort
    [Test]
    public void GlobalVariable_VirtualComPort_IsNotEmpty_Test () {
        var virtualComPort = GlobalVariable.VirtualComPort;

        Assert.IsNotEmpty(virtualComPort);
    }

    [Test]
    public void GlobalVariable_VirtualComPort_IsNotNull_Test()
    {
        var virtualComPort = GlobalVariable.VirtualComPort;

        Assert.IsNotNull(virtualComPort);
    }

    [TestCase(@"COM6")]
    [TestCase(@"Com8")]
    public void GlobalVariable_VirtualComPort_Initialization_Test(string virtualComPort)
    {
        GlobalVariable.VirtualComPort = virtualComPort;
        var expectation = GlobalVariable.VirtualComPort;

        Assert.AreEqual(expectation, virtualComPort);
    }
    #endregion

    #region PathArduinoDriver
    [Test]
    public void GlobalVariable_PathArduinoDriver_IsNotEmpty_Test()
    {
        var pathArduinoDriver = GlobalVariable.PathArduinoDriver;

        Assert.IsNotEmpty(pathArduinoDriver);
    }

    [Test]
    public void GlobalVariable_PathArduinoDriver_IsNotNull_Test()
    {
        var pathArduinoDriver = GlobalVariable.PathArduinoDriver;

        Assert.IsNotNull(pathArduinoDriver);
    }

    [TestCase(@"sketchs/sketch.hex")]
    [TestCase(@"drivers/dpinst-amd64.exe")]
    public void GlobalVariable_PathArduinoDriver_Initialization_Test(string path)
    {
        GlobalVariable.PathArduinoDriver = path;
        var expectation = GlobalVariable.PathArduinoDriver;

        Assert.AreEqual(expectation, path);
    }
    #endregion

    #region PathHex
    [Test]
    public void GlobalVariable_PathHex_IsNotEmpty_Test()
    {
        var pathHex = GlobalVariable.PathHex;

        Assert.IsNotEmpty(pathHex);
    }

    [Test]
    public void GlobalVariable_PathHex_IsNotNull_Test()
    {
        var pathHex = GlobalVariable.PathHex;

        Assert.IsNotNull(pathHex);
    }

    [TestCase(@"drivers/dpinst-amd64.exe")]
    [TestCase(@"drivers/dpinst-amd86.exe")]
    public void GlobalVariable_PathHex_Initialization_Test(string pathHex)
    {
        GlobalVariable.VirtualComPort = pathHex;
        var expectation = GlobalVariable.VirtualComPort;

        Assert.AreEqual(expectation, pathHex);
    }
    #endregion

}
