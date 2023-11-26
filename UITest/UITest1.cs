using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace UITest
{
    public class HomePageTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _screenshotDirectory;
        private readonly string _browser;

        public HomePageTests()
        {
            // Define the browser type in a configuration file or an environment variable
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

            _driver.Manage().Window.Maximize(); // Maximize the browser window

            // Set the screenshot directory to a relative path
            _screenshotDirectory = Path.GetFullPath(
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "ScreenShots"));

            // Ensure the directory for the screenshots exists
            if (!Directory.Exists(_screenshotDirectory))
            {
                Directory.CreateDirectory(_screenshotDirectory);
            }
        }

        [Fact]
        public void HomePage_Title_Should_Match_Expected_In_All_Browsers()
        {
            _driver.Navigate().GoToUrl("https://localhost:63680/");
            Assert.Equal("Index page - YourMobileGuide", _driver.Title);

            // Take a screenshot after checking the title
            TakeScreenshot($"HomePageTitleVerified_{_browser}");

            // Keep the browser open for a set amount of time (e.g., 5 seconds)
            System.Threading.Thread.Sleep(5000);
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
