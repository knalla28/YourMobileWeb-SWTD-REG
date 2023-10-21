using NUnit.Framework;
using YourMobileGuide.Models;

namespace UnitTests.Models
{
    // Unit tests for the CommentModel class
    public class CommentModelTests
    {
        // Instance of CommentModel to be used in the tests
        private CommentModel _testData;

        #region TestSetup
        [SetUp]
        public void TestInitialize()
        {
            // Creating a new instance of CommentModel before each test
            _testData = new CommentModel();
        }

        // Method to clean up any objects or variables after the tests
        [TearDown]
        public void TestCleanup()
        {
            _testData = null;
        }
        #endregion

        #region CommentID
        // Test to validate that we can set and retrieve the Id property of a CommentModel instance
        [Test]
        public void New_Comment_ID_Test_Should_Return_True()
        {
            _testData.Id = "COM_001";

            // Checking that the Id property was correctly set and retrieved
            Assert.AreEqual("COM_001", _testData.Id);
        }

        // Test to validate that each new instance of CommentModel gets its own unique Id
        [Test]
        public void Auto_Generated_ID_Should_Be_Unique()
        {
            // Creating another instance of CommentModel
            var anotherComment = new CommentModel();

            // Checking if the two instances have different Ids
            Assert.AreNotEqual(_testData.Id, anotherComment.Id);
        }
        #endregion

        #region Comment
        // Test to validate that we can set and retrieve the Comment property of a CommentModel instance
        [Test]
        public void New_Comment_Test_Should_Return_True()
        {
            // Setting the Comment property of the CommentModel instance
            _testData.Comment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

            // Checking that the Comment property was correctly set and retrieved
            Assert.AreEqual("Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", _testData.Comment);
        }

        // Test to validate that when we set the Comment property to null, it is correctly retrieved as null
        [Test]
        public void New_Comment_Test_Should_Return_False_When_Comment_Is_Null()
        {
            // Setting the Comment property of the CommentModel instance to null
            _testData.Comment = null;

            // Checking that the Comment property is null
            Assert.IsNull(_testData.Comment);
        }
        #endregion
    }
}
