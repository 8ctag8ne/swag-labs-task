using OpenQA.Selenium;

namespace SwagLabsTask.Utilities
{
    public sealed class WebDriverManager
    {
        private static ThreadLocal<IWebDriver?> threadLocalDriver = new ThreadLocal<IWebDriver?>();

        private WebDriverManager() { }

        public static IWebDriver GetDriver(string browser)
        {
            if (threadLocalDriver.Value == null)
            {
                threadLocalDriver.Value = Browser.CreateWebDriver(browser);
            }
            return threadLocalDriver.Value;
        }

        public static void ResetDriver()
        {
            if (threadLocalDriver.Value != null)
            {
                threadLocalDriver.Value.Quit();
                threadLocalDriver.Value.Dispose();
                threadLocalDriver.Value = null;
            }
        }
    }
}