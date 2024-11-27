using SwagLabsTask.PageObjects;
using Xunit;
using SwagLabsTask.Utilities;


namespace SwagLabsTestTask
{   
    public class LoginSuccessTests
    {
        private readonly string browser = "Chrome";
        public LoginSuccessTests()
        {
            SerilogLogger.LogInfo($"{nameof(LoginSuccessTests)} class has been initialized.");
        }

        [Theory]
        [InlineData("standard_user", "secret_sauce")]
        [InlineData("performance_glitch_user", "secret_sauce")]
        [InlineData("error_user", "secret_sauce")]
        [InlineData("visual_user", "secret_sauce")]
        public void Should_Log_In_Successfully_With_Valid_Credentials(string username, string password)
        {
            // Given: I am on the login page
            using (var logSession = new SerilogSession())
            using (var loginPage = new LoginPage(browser, TimeSpan.FromSeconds(5)))
            using (var inventoryPage = new InventoryPage(browser, TimeSpan.FromSeconds(5)))
            {
                SerilogLogger.LogInfo("Starting test UC3:");
                loginPage.Open();
                // When: I log in with valid credentials
                loginPage.FillUsername(username);
                loginPage.FillPassword(password);
                loginPage.Login();
                Assert.Empty(loginPage.GetErrors());
                inventoryPage.Open();
                // Then: I am redirected to the inventory page
                Assert.Equal(loginPage.GetCurrentUrl(), inventoryPage.GetPageUrl());
                Assert.Contains("Swag Labs", inventoryPage.GetLogoText());
                SerilogLogger.LogInfo($"Test UC3 with values {username}, {password} has passed.");
            }
        }
    }
}