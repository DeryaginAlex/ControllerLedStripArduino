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
        catch (IndexOutOfRangeException)
        {
            result = true;
        }

        Assert.IsTrue(result);
    }

    #endregion

    #region VirtualComPort
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
    #endregion

}
