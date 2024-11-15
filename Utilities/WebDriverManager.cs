using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SwagLabsTask.Utilities
{
    public sealed class WebDriverManager
    {
        private static WebDriverManager instance = null;
        private static readonly object padlock = new object();
        private IWebDriver driver;
        private IBrowserStrategy browserStrategy;

        private WebDriverManager(IBrowserStrategy strategy)
        {
            browserStrategy = strategy;
            driver = browserStrategy.CreateDriver();
        }

        public static WebDriverManager GetInstance(IBrowserStrategy strategy)
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new WebDriverManager(strategy);
                }
                return instance;
            }
        }

        public IWebDriver Driver
        {
            get { return driver; }
        }

        public WebDriverManager Instance
        {
            get { return instance; }
        }

        public void Quit()
        {
            driver.Quit();
        }
    }
}