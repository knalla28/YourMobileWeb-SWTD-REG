using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Xunit;
using SeleniumExtras.WaitHelpers;

namespace UITest
{
    public class ProductCommentTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _screenshotDirectory;
        private readonly string _browser;

        public ProductCommentTests()
        {
            _browser = Environment.GetEnvironmentVariable("BROWSER") ?? "chrome"; // Default to 'chrome' if not set

            switch (_browser.ToLower())
            {
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "edge":
                    _driver = new EdgeDriver();
                    break;
                case "chrome":
                default:
                    _driver = new ChromeDriver();
                    break;
            }

            _driver.Manage().Window.Maximize();

            _screenshotDirectory = Path.GetFullPath(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "ScreenShots"));

            if (!Directory.Exists(_screenshotDirectory))
            {
                Directory.CreateDirectory(_screenshotDirectory);
            }
        }

        [Fact]
        public void Homepage_Sort_View_More_Info_Rate_Comment_Keep_And_Verify()
        {
            _driver.Navigate().GoToUrl("https://localhost:63680/");
            // Waiting for the page to load
            Thread.Sleep(3000);
            TakeScreenshot("HomePageBeforeSorting");

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            // Sorting alphabetically
            IWebElement sortRadioButton = wait.Until(ExpectedConditions.ElementExists(By.Id("sortAlphabetically")));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", sortRadioButton);
            // Waiting for sorting to complete
            Thread.Sleep(3000);
            TakeScreenshot("HomePageAfterSorting");

            // Clicking on "More Info" for iPhone 13
            IWebElement moreInfoButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("moreInfo_iphone_13")));
            moreInfoButton.Click();
            Thread.Sleep(3000);
            TakeScreenshot("iPhone13MoreInfo");

            // Clicking the "Comment" button
            // Check if the button is inside an iframe or not
            try
            {
                // If the element is inside an iframe, switch to it
                // _driver.SwitchTo().Frame("your_iframe_id");

                IWebElement commentButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("AddComment")));
                commentButton.Click();
                Thread.Sleep(3000);
                TakeScreenshot("CommentInputField");

                // Typing the comment "Amazing!" into the input field
                IWebElement commentInputField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("newComment")));
                commentInputField.SendKeys("Amazing!");
                Thread.Sleep(3000);
                TakeScreenshot("CommentEntered");

                // Clicking the "Keep" button
                IWebElement keepButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("keepComment")));
                keepButton.Click();
                Thread.Sleep(3000);
                TakeScreenshot("CommentKept");
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Re-throw the exception to fail the test
            }
            finally
            {
                // Switch back to the main content if we've switched to an iframe
                // _driver.SwitchTo().DefaultContent();
            }
        }

        private void TakeScreenshot(string fileName)
        {
            var screenshotDriver = _driver as ITakesScreenshot;
            var screenshot = screenshotDriver.GetScreenshot();
            string filePath = Path.Combine(_screenshotDirectory, $"{fileName}.png");
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
            Console.WriteLine($"Screenshot saved to: {filePath}");
            Thread.Sleep(3000); // Add delay after taking a screenshot
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
