using OpenQA.Selenium;
using SwagLabsTask.Utilities;

namespace SwagLabsTask.PageObjects
{
    public class InventoryPage : BasePage
    {
        private By LogoLocator = By.XPath("//div[@class='app_logo']");
        public InventoryPage(string browser, TimeSpan timeout) : base(browser, timeout, @"https://www.saucedemo.com/inventory.html"){}

        public InventoryPage Open()
        {
            driver.Url = Url;
            SerilogLogger.LogInfo("Inventory page opened.");
            return this;
        }

        public string GetLogoText()
        {
            try
            {
                return wait.Until(driver => driver.FindElement(LogoLocator)).Text;
            }
            catch (NoSuchElementException ex)
            {
                SerilogLogger.LogError($"Logo not found on inventory page: {ex.Message}");
                throw;
            }
            catch (WebDriverTimeoutException ex)
            {
                SerilogLogger.LogError($"Timeout on inventory page: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                SerilogLogger.LogError($"Unexpected error during visiting inventory page: {ex.Message}");
                throw;
            }
        }
    }
}