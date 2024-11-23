using OpenQA.Selenium;
using SwagLabsTask.Utilities;

namespace SwagLabsTask.PageObjects
{
    public class LoginPage : BasePage
    {
        private By UsernameLocator = By.XPath("//input[@id='user-name']");
        private By PasswordLocator = By.XPath("//input[@id='password']");
        private By LoginLocator = By.XPath("//input[@id='login-button']");

        public LoginPage(string browser, TimeSpan timeout) : base(browser, timeout, @"https://www.saucedemo.com/"){}

        public LoginPage Open()
        {
            try
            {
                driver.Url = Url;
                GetUsernameElement();
                GetPasswordElement();
                SerilogLogger.LogInfo("Login page opened");
                return this;
            }
            catch (Exception ex)
            {
                SerilogLogger.LogError($"Failed to open login page: {ex.Message}");
                throw;
            }
        }

        public IWebElement GetUsernameElement()
        {
            try
            {
                return wait.Until(driver => driver.FindElement(UsernameLocator));
            }
            catch (NoSuchElementException ex)
            {
                SerilogLogger.LogError($"Username field was not found on login page: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                SerilogLogger.LogError($"Unexpected error during visiting login page: {ex.Message}");
                throw;
            }
        }

        public IWebElement GetPasswordElement()
        {
            try
            {
                return wait.Until(driver => driver.FindElement(PasswordLocator));
            }
            catch (NoSuchElementException ex)
            {
                SerilogLogger.LogError($"Password field was not found on login page: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                SerilogLogger.LogError($"Unexpected error during visiting login page: {ex.Message}");
                throw;
            }
        }

        public void FillUsername(string username)
        {
            try
            {
                var usernameElement = GetUsernameElement();
                usernameElement.Clear();
                usernameElement.SendKeys(username);
                SerilogLogger.LogInfo($"Filled username: {username}");
            }
            catch (Exception ex)
            {
                SerilogLogger.LogError($"Failed to fill username: {username}\n Error: {ex.Message}");
                throw;
            }
        }
        
        public void FillPassword(string password)
        {
            try
            {
                var passwordElement = GetPasswordElement();
                passwordElement.Clear();
                passwordElement.SendKeys(password);
                SerilogLogger.LogInfo($"Filled password: {password}");
            }
            catch (Exception ex)
            {
                SerilogLogger.LogError($"Failed to fill password: {password}\n Error: {ex.Message}");
                throw;
            }
        }

        public void ClearUsername()
        {
            try
            {
                var element = GetUsernameElement();
                string value = GetUsernameValue();
                for (int i = 0; i < value.Length; i++)
                {
                    element.SendKeys(Keys.Backspace);
                }
            }
            catch (Exception ex)
            {
                SerilogLogger.LogError($"Failed to clear username: {ex.Message}");
                throw;
            }
        }
        
        public void ClearPassword()
        {
            try
            {
                var element = GetPasswordElement();
                string value = GetPasswordValue();
                for (int i = 0; i < value.Length; i++)
                {
                    element.SendKeys(Keys.Backspace);
                }
            }
            catch (Exception ex)
            {
                SerilogLogger.LogError($"Failed to clear password: {ex.Message}");
                throw;
            }
        }

        public void Login()
        {
            try
            {
                var button = wait.Until(driver => driver.FindElement(LoginLocator));
                button.Click();
                SerilogLogger.LogInfo("Login form submitted.");
            }
            catch (Exception ex)
            {
                SerilogLogger.LogError($"Failed to login: {ex.Message}");
                throw;
            }
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
            try
            {

                List<string> errors = new List<string>();
                var h3 = wait.Until(driver => driver.FindElements(By.XPath("//h3[@data-test='error']")));
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
            catch (Exception ex)
            {
                SerilogLogger.LogError($"Failed to fetch errors: {ex.Message}");
                throw;
            }
        }
        public bool IsErrorDisplayed(string errorMessage)
        {
            var errors = GetErrors();
            return errors.Any(error => error.Contains(errorMessage));
        }
    }
}