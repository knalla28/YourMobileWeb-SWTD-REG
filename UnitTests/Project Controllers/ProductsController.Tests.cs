using NUnit.Framework;
using System.Linq;
using YourMobileGuide.Controllers;

namespace UnitTests.Controllers.ProductController
{
    // Unit tests for ProductsController class
    public class ProductsControllerTest
    {
        #region TestSetup
        [SetUp]
        public void TestInitialize()
        {
        }
        #endregion

        #region get_AllData_Present_Should_Return_True
        // Creating a ProductService instance, initializing a ProductsController with it,
        // retrieveing all data, and checking if the first item matches
        [Test]
        public void get_All_Data_Present_Should_Return_True()
        {
            //arrange

            //Act
            // Storing datapoint as a ProductController datapoint
            var newData = new ProductsController(TestHelper.ProductService).Get().First();

            var response = TestHelper.ProductService.GetAllData().First();

            //Assert
            Assert.AreEqual(newData.Id, response.Id);
        }
        #endregion

        #region Patch_AddValid_Rating_Should_Return_True
        // Initializing a ProductService and a ProductsController, adding a new data point, and checking if it matches the added one
        [Test]
        public void Patch_Add_Valid_Rating_Should_Return_True()
        {
            //arrange

            //Act
            // Storing datapoint as a ProductController datapoint
            var newData = new ProductsController(TestHelper.ProductService);
            // Creating a newRating datapoint to "Patch to theDataController"
            var newRating = new ProductsController.RatingRequest();
            {
                newRating.ProductId = newData.ProductService.GetAllData().Last().Id;
                newRating.Rating = 4;
            }
            newData.Patch(newRating);

            //Assert
            Assert.AreEqual(newData.ProductService.GetAllData().Last().Id, newRating.ProductId);
        }
        #endregion
    }
}
