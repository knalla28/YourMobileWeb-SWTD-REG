using NUnit.Framework;
using System;
using YourMobileGuide.Models;

namespace UnitTests.Models
{
    // Unit testing of ProductTypeEnum.cs
    public class ProductTypeEnumTests
    {
        #region TestSetup
        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region StaticMethodsTests
        [Test]
        [TestCase(2020)]
        [TestCase(2021)]
        [TestCase(2022)]
        [TestCase(2023)]
        public void Valid_Enum_Should_Return_Correct_Name(int year)
        {
            // Arrange
            ProductTypeEnum enumTest = (ProductTypeEnum)Enum.Parse(typeof(ProductTypeEnum), "Year" + year);

            // Act
            var result = enumTest.GetName();

            // Assert
            Assert.AreEqual(true, result.Equals(year.ToString()));
        }

        #endregion StaticMethodTests
    }
}
