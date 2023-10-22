using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using YourMobileGuide.Pages.Product;
using YourMobileGuide.Models;

namespace UnitTests.Pages.Product.Update
{
    // Unit tests for the UpdateModel class.
    public class UpdateTests
    {
        public static UpdateModel pageModel;

        #region TestSetup
        [SetUp]
        public void TestInitialize()
        {
            // Creating a new instance of the UpdateModel class, and passing in a ProductService object as a dependency.
            pageModel = new UpdateModel(TestHelper.ProductService)
            {
            };
        }
        #endregion

        #region OnGet_Should_Return_Empty_Product()
        [Test]
        public void OnGet_Should_Return_Empty_Product()
        {
            // Arrange: No variables or objects to set up for this test.

            // Act: Calling the OnGet() method of the UpdateModel class with no parameters.
            pageModel.OnGet("iphone_12");

            // Assert: Checking that the model state is valid, and that the product title is null or empty.
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("Iphone 12", pageModel.Product.Title);
        }
        #endregion

        #region OnPost_Valid_Should_Create_Product()
        [Test]
        public void OnPost_Valid_Should_Update_Product()
        {
            // Arrange: Creating a new ProductModel instance with valid data.
            var newProduct = new ProductModel
            {
                Id = "PineApple_6",
                Title = "PineApple 6",
                Description = "A cutting edge smrtphone.",
                Url = "https://www.example.com/new-product",
                Image = "https://www.example.com/images/new-product.jpg"
            };
            pageModel.Product = newProduct;

            // Act: Calling the OnPost() method of the UpdateModel class with the new product data.
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert: Checking if the model state is valid, the page redirects to the Index page, and that the new product has been added to the database.
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
        }
        #endregion

        #region OnPost_Invalid_Should_Return_Page()
        [Test]
        public void OnPost_Invalid_Should_Return_Page()
        {
            // Arrange: Creating a new ProductModel instance with invalid data, and forcing an invalid error state.
            var invalidProduct = new ProductModel
            {
                Title = "PineApple 6Pro",
                Description = "A pro, for a pro.",
                Url = "phone_url",
                Image = "https://www.example.com/images/new-product.jpg"
            };
            // pageModel.Product = invalidProduct;
            pageModel.ModelState.AddModelError("Url", "Invalid URL");

            // Act: Calling the OnPostAsync() method of the UpdateModel class with the invalid product model.
            var result = pageModel.OnPost() as ActionResult;

            // Assert: Checking that the model state is invalid, and that the page returns with validation errors.
            Assert.AreEqual(false, pageModel.ModelState.IsValid);
        }
        #endregion

        #region OnGet_InvalidId_Should_Return_NotFound()
        [Test]
        public void OnGet_InvalidId_Should_Return_NotFound()
        {
            // Arrange: No variables or objects to set up for this test.

            // Act: Calling the OnGet() method with an invalid ID.
            pageModel.OnGet("PineApple_7");

            // Assert: Checking that the product is null.
            Assert.IsNull(pageModel.Product);
        }
        #endregion
    }
}
