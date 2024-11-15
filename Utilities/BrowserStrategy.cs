using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace SwagLabsTask.Utilities
{
    public interface IBrowserStrategy
    {
        IWebDriver CreateDriver();
    }
    public class ChromeStrategy : IBrowserStrategy
    {
        public IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();
            return new ChromeDriver(options);
        }
    }

    public class FirefoxStrategy : IBrowserStrategy
    {
        public IWebDriver CreateDriver()
        {
            var options = new FirefoxOptions();
            return new FirefoxDriver(options);
        }
    }
}