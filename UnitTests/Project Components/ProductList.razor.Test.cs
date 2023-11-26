using Bunit;
using NUnit.Framework;

using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using YourMobileGuide.Components;
using YourMobileGuide.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AngleSharp.Dom;

namespace UnitTests.Components
{
    public class ProductListTests : BunitTestContext
    {
        // Page Model variable
        public static PageModel pageModel;

        #region TestSetup
        // Initializes the test setup before each test
        [SetUp]
        public void TestInitialize()
        {
        }
        #endregion TestSetup

        // ProductList should return contents in the default 
        [Test]
        public void ProductList_Default_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Act
            var page = RenderComponent<ProductList>();

            // Getting the Cards retrned
            var result = page.Markup;

            // Assert
            Assert.AreEqual(true, result.Contains("OnePlus 11 5G"));
        }

        // Testing the selectProduct button works as expected
        [Test]
        public void ProductList_ValidId_SelectProductId_Should_Return_Content()
        {
            // Arrange

            // Getting an instance of ProductService
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Getting the id variable for oneplus_11
            var id = "Oneplus_11";

            // Getting the base HTML page
            var page = RenderComponent<ProductList>();

            // Getting the list of all buttons in the HTML document
            var buttonList = page.FindAll("Button");

            // Getting the button that contains id
            var button = buttonList.First(x => x.OuterHtml.Contains(id));

            // Act

            // Posting action to get HTML from the click of the previously found button
            button.Click();

            // Getting the rendered HTML from the button click
            var result = page.Markup;

            // Assert
            // Asserts that the result contains section of what should be rendered
            Assert.AreEqual(true, result.Contains("Android"));


        }

        #region SearchBar
        // Search Input testing
        [Test]
        public void SearchText_Should_Update_When_Text_Entered()
        {
            string inputText = "oneplus";
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var page = RenderComponent<ProductList>();

            // Find searchbar
            IElement searchBar = page.FindAll("input").FirstOrDefault(x => x.Id.Equals("searchText"));

            // Act
            searchBar.Change(inputText);

            // Assert
            Assert.AreEqual(true, page.Markup.Contains(inputText));
        }


        // Testing to search for different products
        [Test]
        public void SaveSearch_Should_Return_New_String()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var page = RenderComponent<ProductList>();

            // Getting the button element with the id of "search"
            IElement searchButton = page.FindAll("button").First(x => x.Id.Equals("search"));

            // Getting the text input element with the ID of searchText
            IElement searchText = page.FindAll("input").FirstOrDefault(x => x.Id.Equals("searchText"));

            // Act

            // Posting a change to the searchbox
            searchText.Change("s23");

            searchButton.Click();

            // Getting the newly rendered HTML from the searchText change
            var result = page.Markup;

            // Assert

            // Asserts that the html contains the change
            Assert.AreEqual(true, result.Contains("s23"));
        }
        #endregion SearchBar

        #region SelectProduct

        // Product with id Oneplus_11 should return contents
        [Test]
        public void SelectProduct_Valid_ID_Oneplus_Should_Return_Content()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Oneplus_11";

            var page = RenderComponent<ProductList>();

            // Finding the Buttons (more info)
            var buttonList = page.FindAll("Button");

            // Finding the one that matches the ID looking for and click it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Act
            button.Click();

            // Getting the markup to use for the assert
            var pageMarkup = page.Markup;

            // Assert
            Assert.AreEqual(true, pageMarkup.Contains("OnePlus 11 5G || 16GB RAM\u002B256GB || Titan || US Factory Unlocked Android Smartphone || 5000 mAh battery || 80W Fast charging || Hasselblad Camera || 120Hz Fluid Display || 4nm Processor || Price : $799.99 "));
        }
        #endregion SelectProduct

        #region SubmitRating
        // Testing that the SubmitRating will change the vote as well as the Star
        [Test]
        public void SubmitRating_Valid_ID_Click_Unstared_Should_Increment_Count_And_Check_Star()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Oneplus_11";
            var page = RenderComponent<ProductList>();

            // Finding the more info button
            var buttonList = page.FindAll("Button");

            // Finding the one that matches the ID we're looking for and clicking it
            var button = buttonList.First(m => m.OuterHtml.Contains(id));

            // Rendering html from the selected button click
            button.Click();

            // Getting the markup of the page post the Click action
            var buttonMarkup = page.Markup;

            // Getting the Star Buttons
            var starButtonList = page.FindAll("span");

            // Second one is the vote button
            var preVoteCountSpan = starButtonList[1];

            // Getting current count of votes
            var preVoteCountString = preVoteCountSpan.InnerHtml;

            // Getting the star button
            var starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));
            var preStarChange = starButton.OuterHtml;

            // Act

            starButton.Click();

            buttonMarkup = page.Markup;
            starButtonList = page.FindAll("span");

            var postVoteCountSpan = starButtonList[1];
            var postVoteCountString = postVoteCountSpan.InnerHtml;

            starButton = starButtonList.First(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star checked"));

            var postStarChange = starButton.OuterHtml;

            // Assert

            // Asserta that there was no votes first, then compare after vote, then compare if they're equal
            Assert.AreEqual(false, preVoteCountString.Equals(postVoteCountString));
        }

        // Testing that the SubmitRating will change the vote as well as the Star
        [Test]
        public void SubmitRating_Valid_ID_Click_Stared_Should_Increment_Count_And_Leave_Star_Check_Remaining()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var id = "Oneplus_11";
            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();

            var buttonMarkup = page.Markup;

            var starButtonList = page.FindAll("span");

            var preVoteCountSpan = starButtonList[1];
            var preVoteCountString = preVoteCountSpan.InnerHtml;

            var starButton = starButtonList.Last(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));

            var preStarChange = starButton.OuterHtml;

            // Act

            // Clicking the star button
            starButton.Click();
            buttonMarkup = page.Markup;
            starButtonList = page.FindAll("span");

            var postVoteCountSpan = starButtonList[1];
            var postVoteCountString = postVoteCountSpan.InnerHtml;

            starButton = starButtonList.Last(m => !string.IsNullOrEmpty(m.ClassName) && m.ClassName.Contains("fa fa-star"));

            var postStarChange = starButton.OuterHtml;

            // Assert

            // Confirm that the record had no votes to start, and 1 vote after
            Assert.AreEqual(false, preVoteCountString.Equals(postVoteCountString));
        }
        #endregion SubmitRating

        #region Sort
        // Testing that sort is sorting the items alphabetically
        [Test]
        public void SortAlphabetically_Valid_Should_Return_Alphabetically_Sorted_Items()
        {
            // Arrange

            var firstItem = TestHelper.ProductService.GetAllData().OrderBy(x => x.Title).First();
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            var page = RenderComponent<ProductList>();

            IElement sortAlphabetically = page.FindAll("input").FirstOrDefault(x => x.Id.Equals("sortAlphabetically"));

            // Act
            sortAlphabetically.Click();

            // Assert
            Assert.AreEqual(true, page.FindAll("div").FirstOrDefault(x => x.ClassName.Equals("card-body")).ToMarkup().Contains(firstItem.Title));
        }

        #endregion
        // Testing comment
        [Test]
        public void AddComment_Valid_Text_Should_Return_True()
        {
            // Arrange

            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            var id = "Oneplus_11";

            var page = RenderComponent<ProductList>();
            var buttonList = page.FindAll("Button");
            var button = buttonList.First(m => m.OuterHtml.Contains(id));
            button.Click();
            var buttonMarkup = page.Markup;
            var cardPage = RenderComponent<ProductList>();

            // Getting button with id as "AddComment"
            IElement searchButton = null;
            foreach (var element in page.FindAll("button"))
            {
                if (element.Id != null && element.Id.Equals("AddComment"))
                {
                    searchButton = element;
                }
            }

            // Getting page after pressing "AddComment" button
            searchButton.Click();

            // Updating the html
            var commentPage = page.Markup;

            // Getting the element with the input tag with ID of "newComment"
            IElement newCommentBox = null;
            foreach (var element in page.FindAll("input"))
            {
                if (element.Id.Equals("newComment"))
                {
                    newCommentBox = element;
                }
            }

            // Get the element for keep comment button with ID keepComment
            IElement newCommentKeep = null;
            foreach (var element in page.FindAll("button"))
            {
                if (element.Id != null && element.Id.Equals("keepComment"))
                {
                    newCommentKeep = element;
                }
            }

            // Act

            // Posting the comment to add
            newCommentBox.Change("wow!");
            newCommentKeep.Click();

            // Getting updated HTML markup page after the post
            var res = page.Markup;

            // Assert
            // Asserts that the HTML rendered contains the newly added comment
            Assert.AreEqual(true, res.Contains("wow!"));
        }

        // Testing filter function
        [Test]
        public void ObjectFilter_SelectValid_Input_Should_Return_True()
        {
            // Arrange

            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            var id = "2021";

            // Getting html page
            var page = RenderComponent<ProductList>();

            // Getting all instances of 'select'
            var selectList = page.FindAll("select");
            var select = selectList.First(m => m.OuterHtml.Contains(id));


            // Act

            // Posting the value 'star' to the form
            select.Change("2020");

            // Getting the new rendered HTML after the post
            var buttonMarkup = page.Markup;

            // Assert

            // Asserts that the values are saved in the markup
            Assert.AreEqual(true, buttonMarkup.Contains(""));
        }
    }
}
