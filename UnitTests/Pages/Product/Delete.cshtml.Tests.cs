using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using YourMobileGuide.Pages.Product;
using YourMobileGuide.Models;

namespace UnitTests.Pages.Product.Delete
{
    // Unit tests for the DeleteModel class.
    public class DeleteTests
    {
        public static DeleteModel pageModel;

        #region TestSetup
        [SetUp]
        public void TestInitialize()
        {
            // Creating a new instance of the DeleteModel class, and passing in a ProductService object as a dependency.
            pageModel = new DeleteModel(TestHelper.ProductService)
            {
            };
        }
        #endregion

        #region OnGet_Valid_Should_Return_Products()
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange: Grabbing the first item for testing purposes.
            var data = TestHelper.ProductService.GetAllData().First();

            // Act: Calling the OnGet() method of the DeleteModel class with a valid product ID.
            pageModel.OnGet(data.Id);

            // Assert: Checking if the model state is valid, and that the product title matches the expected value.
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, data.Id == pageModel.Product.Id);
        }
        #endregion

        #region OnGet_InvalidId_Should_Return_Null()
        [Test]
        public void OnGet_InvalidId_Should_Return_Null()
        {
            // Arrange: An invalid product ID.
            string invalidId = "PineApple_7";

            // Act: Calling the OnGet() method of the DeleteModel class with an invalid product ID.
            pageModel.OnGet(invalidId);

            // Assert: Checking if the model state is valid, and that the product is null.
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(null, pageModel.Product);
        }
        #endregion

        #region OnPost_Valid_Should_Return_Products()
        [Test]
        public void OnPost_Valid_Should_Return_Products()
        {
            // Arrange: Creating dummy data to insert.
            var dummyData = new ProductModel()
            {
                Id = "PineApple_6",
                Title = "PineApple 6",
                Description = "A cutting edge smrtphone.",
                Url = "https://www.example.com/new-product",
                Image = "https://www.example.com/images/new-product.jpg"
            };

            // First, creating a product to delete
            pageModel.Product = TestHelper.ProductService.CreateData(dummyData);

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));

            // Confirming that the item is deleted
            Assert.AreEqual(null, TestHelper.ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(pageModel.Product.Id)));
        }
        #endregion OnPost

        #region OnPost_InvalidId_Should_Return_Null()
        [Test]
        public void OnPost_InvalidId_Should_Return_Null()
        {
            // Arrange: Creating dummy data to insert.
            var dummyData = new ProductModel()
            {
                Id = "PineApple_6",
                Title = "PineApple 6",
                Description = "A cutting edge smrtphone.",
                Url = "https://www.example.com/new-product",
                Image = "https://www.example.com/images/new-product.jpg"
            };

            // First, creating a product to delete
            pageModel.Product = TestHelper.ProductService.CreateData(dummyData);

            // Act: Calling the OnPost() method of the DeleteModel class with an invalid product ID.
            pageModel.Product.Id = "Invalid_Id";
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert: Checking if the model state is valid, and that the product is null.
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, result.PageName.Contains("Index"));
            Assert.AreEqual(null, TestHelper.ProductService.GetAllData().FirstOrDefault(m => m.Id.Equals(dummyData.Id)));
        }
        #endregion

        #region OnPost_NullProduct_Should_Throw_Exception()
        [Test]
        public void OnPost_NullProduct_Should_Throw_Exception()
        {
            // Arrange: Creating dummy data to insert.
            var dummyData = new ProductModel()
            {
                Id = "PineApple_6",
                Title = "PineApple 6",
                Description = "A cutting edge smrtphone.",
                Url = "https://www.example.com/new-product",
                Image = "https://www.example.com/images/new-product.jpg"
            };

            // First, creating a product to delete
            pageModel.Product = TestHelper.ProductService.CreateData(dummyData);

            // Act: Calling the OnPost() method of the DeleteModel class with a null product.
            pageModel.Product = null;

            // Assert: Expecting a System.NullReferenceException.
            Assert.Throws<System.NullReferenceException>(() => pageModel.OnPost());
        }
        #endregion

    }
}
