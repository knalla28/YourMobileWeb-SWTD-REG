using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using YourMobileGuide.Components;
using YourMobileGuide.Services;

namespace UnitTests.Components
{
    // Unit tests for Product Index
    public class ProductIndexTests : BunitTestContext
    {
        #region TestSetup
        [SetUp]
        public void TestInitialize()
        {
        }
        #endregion

        #region ProductIndex_Default_Should_Return_Content()
        [Test]
        public void ProductIndex_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            var page = RenderComponent<ProductIndex>();
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains("iphone_12"));
        }
        #endregion
    }
}
