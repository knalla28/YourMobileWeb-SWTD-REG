using System.Diagnostics;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using YourMobileGuide.Pages;

namespace UnitTests.Pages.Error
{
    /// <summary>
    /// Tests for the ErrorModel class.
    /// </summary>
    public class ErrorTests
    {
        #region TestSetup

        // Holds an instance of the ErrorModel class.
        public static ErrorModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            // Create a mock logger.
            var MockLoggerDirect = Mock.Of<ILogger<ErrorModel>>();

            // Initialize the pageModel object.
            pageModel = new ErrorModel(MockLoggerDirect)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }

        #endregion TestSetup

        #region OnGet

        /// <summary>
        /// Tests the OnGet method when a valid activity is set.
        /// It should return the RequestId.
        /// </summary>
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange
            Activity activity = new Activity("activity");
            activity.Start();

            // Act
            pageModel.OnGet();

            // Reset
            activity.Stop();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(activity.Id, pageModel.RequestId);
        }

        /// <summary>
        /// Tests the OnGet method when an invalid activity is null.
        /// It should return the TraceIdentifier and set ShowRequestId to true.
        /// </summary>
        [Test]
        public void OnGet_InValid_Activity_Null_Should_Return_TraceIdentifier()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("trace", pageModel.RequestId);
            Assert.AreEqual(true, pageModel.ShowRequestId);
        }

        #endregion OnGet
    }
}
