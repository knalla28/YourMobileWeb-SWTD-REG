using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using YourMobileGuide.Pages.Product;
using YourMobileGuide.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UnitTests.Pages.Product.Create
{
    // This class contains unit tests for the CreateModel class.
    public class CreateTests
    {
        #region TestSetup

        public static CreateModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            pageModel = new CreateModel(TestHelper.ProductService)
            {
                
            };
        }

        #endregion TestSetup

        #region OnGet


      
        [Test]
        public void OnGet_Should_Return_New_Product_Model()
        {
            // Arrange:
            var oldCount = TestHelper.ProductService.GetAllData().Count();

            // Act: Calling the OnGet() method of the CreateModel class.
            pageModel.OnGet();

            // Assert: Checking if the model state is valid, and the product model is a new instance with default values.
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, pageModel.Product.Title == "");
        }

        #endregion OnGet

        #region OnPost
        
        // Test that the mock test data is initialized
       
        [Test]
        public void OnPost_Valid_Should_Add_Product()
        {
            // Arrange
            // Create a test data to insert
            var testData = new ProductModel
            {
                Id = "myphone1",
                Title = "myphone15",
                Description = "This phone has got 5000 mAh battery and best resolution camera",
                Url = "https://www.google.com/",
                Image = "https://cdn.thewirecutter.com/wp-content/media/2023/10/androidphones-2048px-4856-2x1-1.jpg?auto=webp&quality=75&crop=2:1&width=980&dpr=2"
            };
            pageModel.Product = testData;

            // Act
            var result = pageModel.OnPost() as RedirectToPageResult;

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(true, pageModel.ProductService.GetAllData().Any(x => x.Id == testData.Id));
        }

        [Test]
        public void OnPost_InValid_Model_Should_Return_Page()
        {
            // Arrange
            // Creating a test data to insert
            var testData = new ProductModel
            {
                Id = "myphone1",
                Title = "myphone15",
                Description = "This phone has got 5000 mAh battery and best resolution camera",
                Url = "https://www.google.com/",
                Image = "https://cdn.thewirecutter.com/wp-content/media/2023/10/androidphones-2048px-4856-2x1-1.jpg?auto=webp&quality=75&crop=2:1&width=980&dpr=2"
            };

            pageModel.Product = testData;

            // Forcing an invalid error state
            pageModel.ModelState.AddModelError("bogus", "bogus error");

            // Act
            // Store the ActionResult of the post? TODO: better understand this line of code or ask professor
            var result = pageModel.OnPost() as ActionResult;
            // Store whether the ModelState is valid for later assert
            var stateIsValid = pageModel.ModelState.IsValid;

            // Reset

            // Assert
            Assert.AreEqual(false, stateIsValid);
        }
        #endregion OnPost
    }
}
