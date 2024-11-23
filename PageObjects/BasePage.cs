using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SwagLabsTask.Utilities;

namespace SwagLabsTask.PageObjects
{
    public class BasePage : IDisposable
    {
        
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;
        protected string Url {get;}

        public BasePage(string browser, TimeSpan timeout, string Url)
        {
            driver = WebDriverManager.Instance.GetDriver(browser);
            wait = new WebDriverWait(driver, timeout);
            this.Url = Url;
        }

        public string GetCurrentUrl()
        {
            return driver.Url;
        }

        public string GetPageUrl()
        {
            return Url;
        }

        public void Close()
        {
            WebDriverManager.Instance.ResetDriver();
        }

        public void Dispose()
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                SerilogLogger.LogError($"Error during resource cleanup: {ex.Message}");
                throw;
            }
        }
    }
}