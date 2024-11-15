using OpenQA.Selenium;
using SwagLabsTask.Utilities;

namespace SwagLabsTask.PageObjects
{
    public class LoginPage : BasePage
    {
        private By UsernameLocator = By.Id("user-name");
        private By PasswordLocator = By.Id("password");
        private By LoginLocator = By.Id("login-button");

        public LoginPage(string browser, TimeSpan timeout) : base(browser, timeout, @"https://www.saucedemo.com/"){}

        public LoginPage Open()
        {
            driver.Url = Url;
            GetUsernameElement();
            GetPasswordElement();
            SerilogLogger.LogInfo("Login page opened");
            return this;
        }

        public IWebElement GetUsernameElement()
        {
            return wait.Until(driver => driver.FindElement(UsernameLocator));
        }

        public IWebElement GetPasswordElement()
        {
            return wait.Until(driver => driver.FindElement(PasswordLocator));
        }

        public void FillUsername(string username)
        {
            var usernameElement = GetUsernameElement();
            usernameElement.Clear();
            usernameElement.SendKeys(username);
            SerilogLogger.LogInfo($"Filled username: {username}");
        }
        
        public void FillPassword(string password)
        {
            var passwordElement = GetPasswordElement();
            passwordElement.Clear();
            passwordElement.SendKeys(password);
            SerilogLogger.LogInfo($"Filled password: {password}");
        }

        public void ClearUsername()
        {
            var element = GetUsernameElement();
            string value = GetUsernameValue();
            for (int i = 0; i < value.Length; i++)
            {
                element.SendKeys(Keys.Backspace);
            }
        }
        
        public void ClearPassword()
        {
            var element = GetPasswordElement();
            string value = GetPasswordValue();
            for (int i = 0; i < value.Length; i++)
            {
                element.SendKeys(Keys.Backspace);
            }
        }

        public void Login()
        {
            var button = wait.Until(driver => driver.FindElement(LoginLocator));
            button.Click();
            SerilogLogger.LogInfo("Login form submitted.");
        }

        public string GetUsernameValue()
        {
            return GetUsernameElement().GetAttribute("value");
        }

        public string GetPasswordValue()
        {
            return GetPasswordElement().GetAttribute("value");
        }

        public List<string> GetErrors()
        {
            List<string> errors = new List<string>();
            var h3 = wait.Until(driver => driver.FindElements(By.CssSelector("[data-test='error']")));
            foreach (var h in h3)
            {
                errors.Add(h.Text);
            }
            if(errors.Count > 0)
            {
                foreach(var error in errors)
                {
                    SerilogLogger.LogError(error);
                }
            }
            else
            {
                SerilogLogger.LogInfo("No errors");
            }
            return errors;
        }
    }
}