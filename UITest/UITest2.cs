using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace UITest
{
    public class ReadPageTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _screenshotDirectory;
        private readonly string _browser;

        public ReadPageTests()
        {
            _browser = Environment.GetEnvironmentVariable("BROWSER") ?? "chrome";

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
        public void TestCompleteScenario()
        {
            // Step 1: Verify Home Page Title
            VerifyTitle("https://localhost:63680/", "Index page - YourMobileGuide", "HomePageTitleVerified");

            // Step 2: Click on the Products hyperlink
            ClickLink("productsButton");

            // Step 3: Verify Products Page Title
            VerifyTitle("https://localhost:63680/Product", "Index page - YourMobileGuide", "ProductsPageTitleVerified");

            // Step 4: Simulate scrolling down on the product page
            ScrollDown(3);

            // Step 5: Navigate to the iPhone 12 product page
            _driver.Navigate().GoToUrl("https://localhost:63680/Product/Read/iphone_12");

            // Step 6: Verify iPhone 12 Read Page Title
            VerifyTitle("https://localhost:63680/Product/Read/iphone_12", "Read page - YourMobileGuide", "ReadPageTitleVerified");
        }

        private void VerifyTitle(string url, string expectedTitle, string screenshotName)
        {
            _driver.Navigate().GoToUrl(url);
            string actualTitle = _driver.Title;

            Assert.Equal(expectedTitle, actualTitle);
            Assert.Equal(expectedTitle, actualTitle); // An additional assertion for better failure message

            // Taking a screenshot after checking the title
            TakeScreenshot($"{screenshotName}_{_browser}");

            // A delay for demonstration purposes
            System.Threading.Thread.Sleep(3000);
        }

        private void ClickLink(string linkId)
        {
            var link = _driver.FindElement(By.Id(linkId));
            link.Click();
            // A delay for demonstration purposes
            System.Threading.Thread.Sleep(2000);
        }

        private void ScrollDown(int scrollCount)
        {
            var jsExecutor = (IJavaScriptExecutor)_driver;
            for (int i = 0; i < scrollCount; i++)
            {
                jsExecutor.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                // A delay for demonstration purposes
                System.Threading.Thread.Sleep(2000);
            }
        }

        private void TakeScreenshot(string fileName)
        {
            var screenshotDriver = _driver as ITakesScreenshot;
            if (screenshotDriver != null)
            {
                var screenshot = screenshotDriver.GetScreenshot();
                string filePath = Path.Combine(_screenshotDirectory, $"{fileName}.png");
                screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
                Console.WriteLine($"Screenshot saved to: {filePath}");
            }
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}
