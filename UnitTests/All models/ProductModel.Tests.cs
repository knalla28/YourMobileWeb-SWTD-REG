using NUnit.Framework;
using YourMobileGuide.Models;

namespace UnitTests.Models
{
    // Unit tests for the ProductModel class
    public class ProductModelTest
    {
        private ProductModel _testData;

        #region TestSetup
        [SetUp]
        public void TestInitialize()
        {
            _testData = new ProductModel();
        }

        [TearDown]
        public void TestCleanup()
        {
            _testData = null;
        }
        #endregion

        #region ToString
        // Test to validate that the ToString method returns a JSON string representation of the product model
        [Test]
        public void ToString_Valid_Test_Should_Return_String()
        {
            // Arrange
            var newData = new ProductModel()
            {
                Id = "dummy",
                Title = "dummy 7",
                Description = "dummy phone 7",
                Url = "https://dummy.com",
                Image = "https://example.com/dummy.jpg"
            };

            // Act
            var result = "{\"Id\":\"dummy\",\"Maker\":null,\"Image\":\"https://example.com/dummy.jpg\",\"Url\":\"https://dummy.com\",\"Title\":\"dummy 7\",\"Description\":\"dummy phone 7\",\"Ratings\":null,\"ProductType\":0,\"Quantity\":null,\"Price\":0,\"CommentList\":[]}";

            // Assert
            Assert.AreEqual(result, newData.ToString());
        }
        #endregion

        #region DeepCopy

        // Test to validate that DeepCopy method creates a new object with the same values as the original object
        [Test]
        public void DeepCopy_Valid_Should_Return_Copy_Of_Original()
        {
            // Arrange

            // Making a product model to copy from
            var originalData = new ProductModel()
            {
                Title = "original",
                Description = "original desc",
                Ratings = new int[] { 3 },
            };

            // Act

            // Making a copy of the data
            var result = originalData.DeepCopy();

            // Assert
            Assert.AreEqual(true, originalData.Title.Equals(result.Title));
            Assert.AreEqual(false, result.Ratings == null);
        }

        // Test to validate that the DeepCopy method creates a new object with the same values as the original object, even when Ratings is null
        [Test]
        public void DeepCopy_Valid_Null_Ratings_Should_Return_Copy_Of_Original()
        {
            // Arrange

            // Making a product model to copy from
            var originalData = new ProductModel()
            {
                Title = "original",
                Description = "original desc",
                Ratings = null,
            };

            // Act

            // Making a copy of the data
            var result = originalData.DeepCopy();

            // Assert
            Assert.AreEqual(true, originalData.Title.Equals(result.Title));
            Assert.AreEqual(true, result.Ratings == null);
        }

        // Test to validate that DeepCopy method creates a new object, and changes to the new object do not affect the original object
        [Test]
        public void DeepCopy_Valid_Should_Return_New_Object()
        {
            // Arrange

            // Making a product model to copy from
            var originalData = new ProductModel()
            {
                Title = "original",
                Description = "original desc",
            };

            // Act

            // Making a copy of the data and change it
            var result = originalData.DeepCopy();
            result.Title = "new copy";

            // Assert
            Assert.AreEqual(false, originalData.Title.Equals(result.Title));
        }
        #endregion DeepCopy
    }
}
