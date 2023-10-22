using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using YourMobileGuide.Pages.Product;
using YourMobileGuide.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UnitTests.Services.JsonFileProductServiceTests
{
    // Unit tests for JsonFileProductService
    public class JsonFileProductServiceTests
    {
        #region TestSetup
        public static UpdateModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            pageModel = new UpdateModel(TestHelper.ProductService);
        }

        #endregion TestSetup

        #region AddRating
        // Retrieving product data, posting a valid rating, and verifying the accuracy of the last added data
        [Test]
        public void AddRating_Valid_Product_Id_Rating_null_Should_Return_new_Array()
        {
            // Arrange
            // Getting the Last data item
            var data = TestHelper.ProductService.GetAllData().Last();

            // Act
            // Storing the result of the AddRating method (which is being tested)
            var result = TestHelper.ProductService.AddRating(data.Id, 0);

            // Assert
            Assert.AreEqual(true, result);
        }

        // Posting data that doesn't meet function constraints and testing if it gets added
        [Test]
        public void AddRating_Invalid_Product_ID_Not_Present_Should_Return_False()
        {
            // Arrange

            // Act
            // Storing the result of the AddRating method (which is being tested)
            var result = TestHelper.ProductService.AddRating("1000", 3);

            // Assert
            Assert.AreEqual(false, result);
        }

        // Retrieving result of false ID entered data and checking if the result equals the added data
        [Test]
        public void AddRating_InValid_Product_ID_Null_Should_Return_False()
        {
            // Arrange

            // Act
            // Storing the result of the AddRating method (which is being tested)
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        // Retrieving original data list and posting a rating with invalid number of stars to the data
        [Test]
        public void AddRating_InValid_Product_Rating_more_5_Should_Return_False()
        {
            // Arrange
            // Getting first data item
            var data = TestHelper.ProductService.GetAllData().First();

            // Act
            // Storing the result of the AddRating method (which is being tested)
            var result = TestHelper.ProductService.AddRating(data.Id, 6);

            // Assert
            Assert.AreEqual(false, result);
        }

        // Retrieving original ratings, posting a rating against the constraint <=0, and comparing the rating to see if it’s added correctly
        [Test]
        public void AddRating_InValid_Product_Rating_less_than_0_Should_Return_False()
        {
            // Arrange
            // Getting the first data item
            var data = TestHelper.ProductService.GetAllData().First();

            // Act
            // Storing the result of the AddRating method (which is being tested)
            var result = TestHelper.ProductService.AddRating(data.Id, -5);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_Valid_Product_Id_Rating_Not_Null_Should_Return_True()
        {
            // Arrange
            // Getting the first data item
            var data = TestHelper.ProductService.GetAllData().First();

            // Act
            // Storing the result of the AddRating method (which is being tested)
            var result = TestHelper.ProductService.AddRating(data.Id, 3);

            // Assert
            Assert.AreEqual(true, result);
        }

        #endregion AddRating

        #region UpdateData
        // Creating a new default data object with parameters assigned an invalid ID, and testing if it’s correctly added
        [Test]
        public void Update_Data_Invalid_Product_Id_Should_Return_True()
        {
            // Arrange
            var newData = new ProductModel()
            {
                Id = "SWTD-ID-0001",
                Title = "Test",
                Description = "This is a test.",
                Url = "https://example.com",
                Image = "https://example.en.jpg",
            };

            //Act
            // Storing the result of the UpdateData method (which is being tested)
            var result = TestHelper.ProductService.UpdateData(newData);

            // Assert
            Assert.IsNull(result);
        }

        // Creating a new default data object with parameters assigned a valid ID, retrieving the data list, and testing if it’s correctly added
        [Test]
        public void Update_Data_Valid_Product_Id_Should_Return_True()
        {
            // Arrange
            var newData = new ProductModel()
            {
                Id = "iphone_13",
                Title = "iPhone 13 Pro",
                Description = "This is a test.",
                Url = "https://example.com",
                Image = "https://example.en.jpg",
            };

            //Act
            // Storing the result of the UpdateData method (which is being tested)
            TestHelper.ProductService.UpdateData(newData);
            // Storing the updated product for comparison
            var updatedData = TestHelper.ProductService.GetAllData()
                .FirstOrDefault(x => x.Id.Equals(newData.Id));

            // Assert
            Assert.AreEqual(updatedData.Title, newData.Title);
            Assert.AreEqual(updatedData.Description, newData.Description);
        }

        [Test]
        public void Update_Data_Invalid_Product_Model_Should_Return_Null()
        {
            // Arrange
            var newData = new ProductModel();

            //Act
            // Storing the result of the UpdateData method (which is being tested)
            var result = TestHelper.ProductService.UpdateData(newData);

            // Assert
            Assert.IsNull(result);
        }

        #endregion UpdateData

        #region DeleteData
        // Retrieving the data list, posting delete data, and checking if the deleted data is in the result
        [Test]
        public void DeleteData_Valid_Product_Id_Should_Return_True()
        {
            // Arrange
            // Getting the first data item
            var data = TestHelper.ProductService.GetAllData().First();

            // Act
            // Storing the result of the DeleteData method (which is being tested)
            var result = TestHelper.ProductService.DeleteData(data.Id);

            // Assert
            Assert.AreEqual(true, result.Id.Contains(data.Id));
        }

        [Test]
        public void DeleteData_Invalid_Product_Id_Should_Return_Null()
        {
            // Arrange

            // Act
            // Storing the result of the DeleteData method (which is being tested)
            var result = TestHelper.ProductService.DeleteData("invalid_id");

            // Assert
            Assert.AreEqual(null, result);
        }

        #endregion DeleteData

        #region CreateData
        // Retrieving original data list, posting new ProductService object with default parameters, caching lengths of both lists, and comparing the lengths
        [Test]
        public void CreateData_Adding_Should_Return_Larger_ProductList()
        {
            // Arrange
            // Getting the count of the current product list
            var oldProductCount = TestHelper.ProductService.GetAllData().Count();
            // Creating dummy data to insert
            var dummyData = new ProductModel()
            {
                Id = System.Guid.NewGuid().ToString(),
                Title = "myPhone 15",
                Description = "Brand new myPhone base model",
                Url = "https://example.com",
                Image = "https://example.en.jpg",
            };

            // Act
            // Storing the result of the CreateData method (which is being tested)
            var result = TestHelper.ProductService.CreateData(dummyData);
            // Storing the count of the old product list for comparison
            int newProductCount = TestHelper.ProductService.GetAllData().Count();

            // Assert
            Assert.AreEqual(true, newProductCount > oldProductCount);
        }

        #endregion CreateData
    }
}
