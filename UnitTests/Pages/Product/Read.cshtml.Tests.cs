using NUnit.Framework;
using YourMobileGuide.Pages.Product;

namespace UnitTests.Pages.Product.Read
{
    // Unit tests for read functionality
    public class ReadTests
    {
        public static ReadModel pageModel;

        #region TestSetup
        [SetUp]
        public void TestInitialize()
        {
            // Creating a new instance of the ReadModel class with a ProductService object as a dependency
            pageModel = new ReadModel(TestHelper.ProductService);
        }
        #endregion

        #region OnGet_Valid_Should_Return_AllProducts
        // Defining a test for the OnGet method when called with a valid product ID
        [Test]
        public void OnGet_Valid_Should_Return_AllProducts()
        {
            pageModel.OnGet("iphone_13");

            // Checking that the model state is valid and that the correct product is retrieved
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("Iphone 13", pageModel.Product.Title);
        }
        #endregion

        #region OnGet_Invalid_Should_Return_NullProduct
        // Defining a test for the OnGet method when called with an invalid product ID
        [Test]
        public void OnGet_Invalid_Should_Return_NullProduct()
        {
            pageModel.OnGet("myPhone_15");

            // Checking that the model state is valid and that no product is retrieved
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.IsNull(pageModel.Product);
        }
        #endregion

        #region OnGet_Empty_Should_Return_NullProduct
        // Defining a test for the OnGet method when called with an empty string as the product ID
        [Test]
        public void OnGet_Empty_Should_Return_NullProduct()
        {
            pageModel.OnGet("");

            // Checking that the model state is valid and that no product is retrieved
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.IsNull(pageModel.Product);
        }
        #endregion
    }
}
