using Bunit;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// Test Context used by bUnit
    /// </summary>
    public abstract class BunitTestContext : TestContextWrapper
    {
        /// <summary>
        /// Sets up the test context.
        /// </summary>
        [SetUp]
        public void Setup() => TestContext = new Bunit.TestContext();

        /// <summary>
        /// Tears down the test context and frees up system resources.
        /// </summary>
        [TearDown]
        public void TearDown() => TestContext.Dispose();
    }
}
