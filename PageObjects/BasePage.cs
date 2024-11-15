using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SwagLabsTask.Utilities;

namespace SwagLabsTask.PageObjects
{
    public class BasePage
    {
        
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;
        protected string Url {get;}

        public BasePage(string browser, TimeSpan timeout, string Url)
        {
            this.driver = WebDriverManager.GetInstance(browser).Driver;
            wait = new WebDriverWait(driver, timeout);
            this.Url = Url;
        }

        public string GetCurrentUrl()
        {
            return driver.Url;
        }

        public string GetPageUrl()
        {
            return this.Url;
        }

        public void Close()
        {
            driver.Quit();
            WebDriverManager.ResetInstance();
        }
    }
}