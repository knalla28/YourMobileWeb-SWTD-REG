using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Xunit;
using System.Threading; // Needed for Thread.Sleep
using SeleniumExtras.WaitHelpers;

namespace UITest
{
    public class ProductRatingTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _screenshotDirectory;
        private readonly string _browser;

        public ProductRatingTests()
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
        public void Homepage_Sort_View_More_Info_Rate_And_Verify()
        {
            _driver.Navigate().GoToUrl("https://localhost:63680/");
            Thread.Sleep(3000); // Wait for the page to load
            TakeScreenshot("HomePageBeforeSorting");

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            // Sort alphabetically
            IWebElement sortRadioButton = wait.Until(ExpectedConditions.ElementExists(By.Id("sortAlphabetically")));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", sortRadioButton);
            Thread.Sleep(3000); // Wait for sorting to complete
            TakeScreenshot("HomePageAfterSorting");

            // Click "More Info" for iPhone 12
            IWebElement moreInfoButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("moreInfo_iphone_12")));
            moreInfoButton.Click();
            Thread.Sleep(3000); // Wait for the modal to open
            TakeScreenshot("iPhone12MoreInfo");

            // Rate the iPhone 12 as 5 stars
            // The actual implementation might require targeting the specific stars within a container element
            var ratingStars = _driver.FindElements(By.CssSelector("#productModal .fa-star"));
            if (ratingStars.Count >= 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    ratingStars[i].Click(); // Click on each of the first five stars
                }
            }
            Thread.Sleep(3000); // Wait for the rating to be processed
            TakeScreenshot("iPhone12Rated");

            // Add any further steps here as necessary
        }

        private void TakeScreenshot(string fileName)
        {
            var screenshotDriver = _driver as ITakesScreenshot;
            var screenshot = screenshotDriver.GetScreenshot();
            string filePath = Path.Combine(_screenshotDirectory, $"{fileName}.png");
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
            Console.WriteLine($"Screenshot saved to: {filePath}");
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
