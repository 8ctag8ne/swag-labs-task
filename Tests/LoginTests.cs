using SwagLabsTask.PageObjects;
using Xunit;
using SwagLabsTask.Utilities;

namespace SwagLabsTestTask
{   
    public class LoginTests
    {
        private string browser = "chrome"; //cuz it's the fastest on my computer
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
            var loginPage = new LoginPage(browser, TimeSpan.FromSeconds(5));
            try
            {
                loginPage.Open();
                loginPage.FillUsername(username);
                loginPage.FillPassword(password);
                loginPage.ClearUsername();
                loginPage.ClearPassword();
                loginPage.Login();
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
            var loginPage = new LoginPage(browser, TimeSpan.FromSeconds(5));
            try
            {
                loginPage.Open();
                loginPage.FillUsername(username);
                loginPage.FillPassword(password);
                loginPage.ClearPassword();
                loginPage.Login();
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
            var loginPage = new LoginPage(browser, TimeSpan.FromSeconds(5));
            var inventoryPage = new InventoryPage(browser, TimeSpan.FromSeconds(5));
            try
            {
                loginPage.Open();
                loginPage.FillUsername(username);
                loginPage.FillPassword(password);
                loginPage.Login();
                Assert.Empty(loginPage.GetErrors());
                inventoryPage.Open();
                Assert.Equal(loginPage.GetCurrentUrl(), inventoryPage.GetPageUrl());
                Assert.Contains("Swag Labs", inventoryPage.GetLogoText());
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