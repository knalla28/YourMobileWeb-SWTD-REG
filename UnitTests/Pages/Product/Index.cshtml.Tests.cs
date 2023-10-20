using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using YourMobileGuide.Pages.Product;
using YourMobileGuide.Models;

namespace UnitTests.Pages.Product.Index
{
    /// This class contains unit tests for the IndexModel class.
    
    public class IndexTests
    {
        #region TestSetup

        // This field holds a reference to an instance of the IndexModel class
        public static IndexModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            // Let's create a new instance of the IndexModel class, and pass in a ProductService object as a dependency.
            pageModel = new IndexModel(TestHelper.ProductService)
            {
                // There are no additional initialization steps for the IndexModel instance in this case
            };
        }
        #endregion TestSetup

        #region OnGet
        /// This method tests the OnGet() method of the IndexModel class when called valid.
        [Test]
        public void OnGet_Valid_Should_Return_AllProducts()
        {
            // Arrange: There are no variables or objects to set up for this test.

            // Act: Call the OnGet() method of the IndexModel class with no search string.
            pageModel.OnGet();

            // Assert: Checking that the model state is valid, and that the product list contains all products in the database.
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(TestHelper.ProductService.GetAllData().Count(), pageModel.Products.Count());
        }
        #endregion OnGet
    }
}
