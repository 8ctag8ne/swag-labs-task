using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SwagLabsTask.Utilities;

namespace SwagLabsTask.PageObjects
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver, TimeSpan timeout) : base(driver, timeout, @"https://www.saucedemo.com/"){}
        
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
            return wait.Until(driver => driver.FindElement(By.Id("user-name")));
        }

        public IWebElement GetPasswordElement()
        {
            return wait.Until(driver => driver.FindElement(By.Id("password")));
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

        public void Submit()
        {
            var button = wait.Until(driver => driver.FindElement(By.Id("login-button")));
            button.Click();
            SerilogLogger.LogInfo("Form submitted.");
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