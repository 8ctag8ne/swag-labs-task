using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SwagLabsTask.Utilities;

namespace SwagLabsTask.PageObjects
{
    public class InventoryPage : BasePage
    {
        public InventoryPage(IWebDriver driver, TimeSpan timeout) : base(driver, timeout, @"https://www.saucedemo.com/inventory.html"){}

        public InventoryPage Open()
        {
            driver.Url = Url;
            SerilogLogger.LogInfo("Inventory page opened.");
            return this;
        }

        public string GetLogo()
        {
            return wait.Until(driver => driver.FindElement(By.ClassName("app_logo"))).Text;
        }
    }
}