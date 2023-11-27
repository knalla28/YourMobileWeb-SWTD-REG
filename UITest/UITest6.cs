using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace UITest
{
    public class DarkMode_PrivacyNavigationTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly string _screenshotDirectory;
        private readonly string _browser;

        public DarkMode_PrivacyNavigationTests()
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

            // Step 2: Enable Dark Mode
            EnableDarkMode();

            // Step 3: Take a screenshot after enabling Dark Mode
            TakeScreenshot("DarkModeEnabled");

            // Step 4: Navigate to the Privacy page
            NavigateToPrivacyPage();

            // Step 5: Verify Privacy Page Title
            VerifyPrivacyPageTitle("https://localhost:63680/Privacy", "Privacy Policy - YourMobileGuide", "PrivacyPageTitleVerified");

            // Step 6: Scroll to the bottom of the Privacy page
            ScrollToBottom();

            // Step 7: Take a screenshot after scrolling
            TakeScreenshot("PrivacyPageScrolled");

            System.Threading.Thread.Sleep(3000);
        }

        private void EnableDarkMode()
        {
            // Finding and clicking the Dark Mode button
            var darkModeButton = _driver.FindElement(By.Id("themeSwitchDark"));
            darkModeButton.Click();
            System.Threading.Thread.Sleep(2000);
        }

        private void NavigateToPrivacyPage()
        {
            // Clicking on the Privacy hyperlink
            ClickLink("privacyButton");
        }

        private void ScrollToBottom()
        {
            var jsExecutor = (IJavaScriptExecutor)_driver;
            jsExecutor.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
            System.Threading.Thread.Sleep(2000);
        }

        private void VerifyTitle(string url, string expectedTitle, string screenshotName)
        {
            _driver.Navigate().GoToUrl(url);
            string actualTitle = _driver.Title;

            Assert.Equal(expectedTitle, actualTitle);
            Assert.Equal(expectedTitle, actualTitle); // An additional assertion for a better failure message

            // Taking a screenshot after checking the title
            TakeScreenshot($"{screenshotName}_{_browser}");

            System.Threading.Thread.Sleep(3000);
        }

        private void VerifyPrivacyPageTitle(string url, string expectedTitle, string screenshotName)
        {
            _driver.Navigate().GoToUrl(url);
            string actualTitle = _driver.Title;

            Assert.Equal(expectedTitle, actualTitle);
            Assert.Equal(expectedTitle, actualTitle);

            TakeScreenshot($"{screenshotName}_{_browser}");

            System.Threading.Thread.Sleep(3000);
        }

        private void ClickLink(string linkId)
        {
            var link = _driver.FindElement(By.Id(linkId));
            link.Click();
            System.Threading.Thread.Sleep(2000);
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
