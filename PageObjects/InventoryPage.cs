using OpenQA.Selenium;
using SwagLabsTask.Utilities;

namespace SwagLabsTask.PageObjects
{
    public class InventoryPage : BasePage
    {
        private By LogoLocator = By.ClassName("app_logo");
        public InventoryPage(IWebDriver driver, TimeSpan timeout) : base(driver, timeout, @"https://www.saucedemo.com/inventory.html"){}

        public InventoryPage Open()
        {
            driver.Url = Url;
            SerilogLogger.LogInfo("Inventory page opened.");
            return this;
        }

        public string GetLogoText()
        {
            return wait.Until(driver => driver.FindElement(LogoLocator)).Text;
        }
    }
}