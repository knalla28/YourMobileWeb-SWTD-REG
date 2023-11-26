using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace UITest
{
    public class SearchFunctionalityTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _screenshotDirectory;
        private readonly string _browser;

        public SearchFunctionalityTests()
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
        public void Search_Samsung_And_Verify_Results()
        {
            _driver.Navigate().GoToUrl("https://localhost:63680/");
            Thread.Sleep(3000); // Delay before taking a screenshot
            TakeScreenshot("HomePageBeforeSearch");

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            IWebElement searchInput = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("searchText")));
            Thread.Sleep(3000); // Delay before entering text
            searchInput.SendKeys("Samsung");

            // Click the search button to initiate the search
            IWebElement searchButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("search")));
            Thread.Sleep(3000); // Delay before clicking the search button
            searchButton.Click();

            // Assuming you need to wait for search results after clicking the search button
            Thread.Sleep(3000); // Delay after search before taking a screenshot
            TakeScreenshot("SearchResultsForSamsung");
        }

        private void TakeScreenshot(string fileName)
        {
            var screenshotDriver = _driver as ITakesScreenshot;
            Thread.Sleep(3000); // Delay before taking each screenshot
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
