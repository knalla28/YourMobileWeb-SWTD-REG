using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Hosting;
using YourMobileGuide;

public class ProgramTests
{
    // Declaring mocks for IHost and IHostBuilder
    private Mock<IHost> _hostMock;
    private Mock<IHostBuilder> _builderMock;

    #region TestSetup
    [SetUp]
    public void Setup()
    {
        // Initializing mocks
        _hostMock = new Mock<IHost>(MockBehavior.Strict);
        _builderMock = new Mock<IHostBuilder>(MockBehavior.Strict);
    }

    [TearDown]
    public void TestCleanup()
    {
        // Verifying all expectations on the mocks were met
        _hostMock.VerifyAll();
        _builderMock.VerifyAll();
    }
    #endregion

    #region CreateHostBuilder_ReturnsIHostBuilder()
    [Test]
    public void CreateHostBuilder_ReturnsIHostBuilder()
    {
        // Act: Calling the CreatHostBuilder method
        var result = Program.CreateHostBuilder(new string[0]);

        // Assert: Verifying the result
        Assert.IsInstanceOf<IHostBuilder>(result);
    }
    #endregion
}
