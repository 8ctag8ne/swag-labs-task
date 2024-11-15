using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SwagLabsTask.PageObjects
{
    public class BasePage
    {
        
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;
        protected string Url {get;}

        public BasePage(IWebDriver driver, TimeSpan timeout, string Url)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
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
        }
    }
}