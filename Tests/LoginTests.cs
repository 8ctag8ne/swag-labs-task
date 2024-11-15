using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using SwagLabsTask.PageObjects;
using Xunit;
using Xunit.Abstractions;
using System.Reflection;
using SwagLabsTask.Utilities;

namespace SwagLabsTestTask
{   
    public class LoginTests
    {
        public LoginTests()
        {
            SerilogLogger.LogInfo($"{nameof(LoginTests)} class has been initialized.");
        }

        [Theory]
        [InlineData("Rudolf", "Rittler")]
        [InlineData("", "101244")]
        [InlineData("0", "")]
        [InlineData("", "")]
        public void UC1(string username, string password)
        {
            SerilogLogger.Initialize();
            SerilogLogger.Separate();
            SerilogLogger.LogInfo("Starting test UC1:");
            var loginPage = new LoginPage(new ChromeDriver(), TimeSpan.FromSeconds(5));
            try
            {
                loginPage.Open();
                loginPage.FillUsername(username);
                loginPage.FillPassword(password);
                loginPage.ClearUsername();
                loginPage.ClearPassword();
                loginPage.Submit();
                var errors = loginPage.GetErrors();
                Assert.NotEmpty(errors);
                Assert.Contains("Username is required", errors[0]);
                SerilogLogger.LogInfo($"Test UC1 with values {username}, {password} has passed.");
            }
            catch (Exception ex)
            {
                SerilogLogger.LogInfo(ex.Message);
                throw;
            }
            finally
            {
                loginPage.Close();
                SerilogLogger.CloseAndFlush();
            }
        }

        [Theory]
        [InlineData("Man...", "(Linux)")]
        [InlineData("0", "    ")]
        [InlineData("     ", "")]
        [InlineData("drop table users;", ")")]
        public void UC2(string username, string password)
        {
            SerilogLogger.Initialize();
            SerilogLogger.Separate();
            SerilogLogger.LogInfo("Starting test UC2:");
            var loginPage = new LoginPage(new ChromeDriver(), TimeSpan.FromSeconds(5));
            try
            {
                loginPage.Open();
                loginPage.FillUsername(username);
                loginPage.FillPassword(password);
                loginPage.ClearPassword();
                loginPage.Submit();
                var errors = loginPage.GetErrors();
                Assert.NotEmpty(errors);
                Assert.Contains("Password is required", errors[0]);
                SerilogLogger.LogInfo($"Test UC2 with values {username}, {password} has passed.");
            }
            catch (Exception ex)
            {
                SerilogLogger.LogInfo(ex.Message);
                throw;
            }
            finally
            {
                loginPage.Close();
                SerilogLogger.CloseAndFlush();
            }
        }

        [Theory]
        [InlineData("standard_user", "secret_sauce")]
        [InlineData("performance_glitch_user", "secret_sauce")]
        [InlineData("error_user", "secret_sauce")]
        [InlineData("visual_user", "secret_sauce")]
        public void UC3(string username, string password)
        {
            SerilogLogger.Initialize();
            SerilogLogger.Separate();
            SerilogLogger.LogInfo("Starting test UC3:");
            var driver = new ChromeDriver();
            var loginPage = new LoginPage(driver, TimeSpan.FromSeconds(5));
            var inventoryPage = new InventoryPage(driver, TimeSpan.FromSeconds(5));
            try
            {
                loginPage.Open();
                loginPage.FillUsername(username);
                loginPage.FillPassword(password);
                loginPage.Submit();
                Assert.Empty(loginPage.GetErrors());
                inventoryPage.Open();
                Assert.Equal(loginPage.GetCurrentUrl(), inventoryPage.GetPageUrl());
                Assert.Contains("Swag Labs", inventoryPage.GetLogo());
                SerilogLogger.LogInfo($"Test UC3 with values {username}, {password} has passed.");
            }
            catch (Exception ex)
            {
                SerilogLogger.LogInfo(ex.Message);
                throw;
            }
            finally
            {
                loginPage.Close();
                inventoryPage.Close();
                SerilogLogger.CloseAndFlush();
            }
        }
    }
}