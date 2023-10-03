﻿using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourMobileGuide.Pages;

namespace UnitTests.Pages.Privacy
{
    /// <summary>
    /// Unit testing for Privacy Page
    /// </summary>
    public class PrivacyTests
    {
        #region TestSetup
        //Create local privacy model to test functions.
        public static PrivacyModel pageModel;

        /// <summary>
        /// Set up test intialize
        /// </summary>        
        [SetUp]
        public void TestInitialize()
        {
            //Create mock object of privacymodel logger
            var MockLoggerDirect = Mock.Of<ILogger<PrivacyModel>>();

            //Initialize the privacy model.
            pageModel = new PrivacyModel(MockLoggerDirect)
            {
                PageContext = TestHelper.PageContext,
                TempData = TestHelper.TempData,
            };
        }

        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Checking privacy page working
        /// </summary>
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
        }

        #endregion OnGet
    }
}
