using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace SwagLabsTask.Utilities
{
    public static class Browser
    {
        public static IWebDriver CreateWebDriver(string browser)
        {
            switch (browser.ToLower())
            {
                case "chrome":
                {
                    var options = new ChromeOptions();
                    options.AddArgument("--start-maximized");
                    options.AddArgument("--disable-extensions");
                    return new ChromeDriver(options);
                }
                case "firefox":
                {
                    var options = new FirefoxOptions();
                    options.AddArgument("--start-maximized");
                    return new FirefoxDriver(options);
                }
                case "edge":
                {
                    var options = new EdgeOptions();
                    options.AddArgument("--start-maximized");
                    return new EdgeDriver(options);
                }
                default:
                    throw new NotSupportedException($"Browser {browser} is not supported.");
            }
        }
    }
}