using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Xunit;
using System.Threading;
using SeleniumExtras.WaitHelpers;

namespace UITest
{
    public class ProductCreationTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _screenshotDirectory;
        private readonly string _browser;

        public ProductCreationTests()
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
        public void CreateNewProduct_And_Verify()
        {
            // Navigate to the home page and click the 'Products' link
            _driver.Navigate().GoToUrl("https://localhost:63680/");
            TakeScreenshot("HomePageBeforeClickProducts");
            Thread.Sleep(3000);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            // Click the 'Products' link
            IWebElement productsButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("productsButton")));
            productsButton.Click();
            TakeScreenshot("ProductsPageAfterClickProducts");
            Thread.Sleep(3000);

            // Click the 'Create New' button
            IWebElement createButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("a.btn.btn-green.btn-success.create-button")));
            createButton.Click();
            TakeScreenshot("CreatePageAfterClickCreateNew");
            Thread.Sleep(3000);

            // Enter the product details in the form
            _driver.FindElement(By.Id("Product_Title")).SendKeys("iPhone 15 pro");
            TakeScreenshot("FormAfterTitle");
            Thread.Sleep(3000);
            _driver.FindElement(By.Id("Product_Description")).SendKeys("The iPhone 15 pro is the latest and most advanced model featuring the A17 chip.");
            TakeScreenshot("FormAfterDescription");
            Thread.Sleep(3000);
            _driver.FindElement(By.Id("Product_Image")).SendKeys("https://media.wired.com/photos/6508ec5709b580a9d232c71f/master/w_1920,c_limit/iPhone-15-Pro-Review-Top-Gear.jpg");
            TakeScreenshot("FormAfterImage");
            Thread.Sleep(3000);
            _driver.FindElement(By.Id("Product_Url")).SendKeys("https://www.apple.com/shop/buy-iphone/iphone-15-pro/6.1-inch-display-256gb-blue-titanium-unlocked");
            TakeScreenshot("FormAfterUrl");
            Thread.Sleep(3000);

            // Select the year from the dropdown
            new SelectElement(_driver.FindElement(By.Id("Product_ProductType"))).SelectByValue("3");
            TakeScreenshot("FormAfterYear");
            Thread.Sleep(3000);

            // Click the 'Save' button to submit the form
            IWebElement saveButton = _driver.FindElement(By.CssSelector("input[type='submit'][value='Save']"));
            saveButton.Click();
            TakeScreenshot("FormAfterSave");
            Thread.Sleep(3000);

            // You can include an assertion or verification step here if needed
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
