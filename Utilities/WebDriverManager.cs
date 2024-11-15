using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;


namespace SwagLabsTask.Utilities
{
    public sealed class WebDriverManager
    {
        private static WebDriverManager? instance = null;
        private static readonly object padlock = new object();
        private IWebDriver driver;

        private WebDriverManager(string browser)
        {
            driver = Browser.CreateWebDriver(browser);
        }

        public static WebDriverManager GetInstance(string browser)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new WebDriverManager(browser);
                }
                return instance;
            }
        }

        public IWebDriver Driver
        {
            get { return driver; }
        }

        public WebDriverManager? Instance
        {
            get { return instance; }
        }

        public static void ResetInstance()
        {
            lock (padlock)
            {
                if (instance != null)
                {
                    instance.Driver.Quit();
                    instance = null;
                }
            }
        }
    }
}