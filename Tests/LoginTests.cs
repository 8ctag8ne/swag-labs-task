using SwagLabsTask.PageObjects;
using Xunit;
using SwagLabsTask.Utilities;

[assembly: CollectionBehavior(DisableTestParallelization = false, MaxParallelThreads = 4)]

namespace SwagLabsTestTask
{   
    [Collection("Parallel Tests")]
    public class LoginUsernameTests
    {
        private readonly string browser = "edge";
        public LoginUsernameTests()
        {
            SerilogLogger.LogInfo($"{nameof(LoginUsernameTests)} class has been initialized.");
        }

        [Theory]
        [InlineData("Rudolf", "Rittler")]
        [InlineData("", "101244")]
        [InlineData("0", "")]
        [InlineData("", "")]
        public void Should_Show_Error_When_Username_Is_Missing(string username, string password)
        {
            // Given: I am on the login page
            using (var logSession = new SerilogSession())
            using (var loginPage = new LoginPage(browser, TimeSpan.FromSeconds(5)))
            {
                SerilogLogger.LogInfo("Starting test UC1:");
                loginPage.Open();
                // When: I try to log in without credentials
                loginPage.FillUsername(username);
                loginPage.FillPassword(password);
                loginPage.ClearUsername();
                loginPage.ClearPassword();
                loginPage.Login();
                //Then: I see an error indicating that the username is required
                Assert.True(loginPage.IsErrorDisplayed("Username is required"));
                SerilogLogger.LogInfo($"Test UC1 with values {username}, {password} has passed.");
            }
        }
    }

    [Collection("Parallel Tests")]
    public class LoginPasswordTests
    {
        private readonly string browser = "Chrome";
        public LoginPasswordTests()
        {
            SerilogLogger.LogInfo($"{nameof(LoginPasswordTests)} class has been initialized.");
        }

        [Theory]
        [InlineData("Man...", "(Linux)")]
        [InlineData("0", "    ")]
        [InlineData("     ", "")]
        [InlineData("drop table users;", ")")]
        public void Should_Show_Error_When_Password_Is_Missing(string username, string password)
        {
            // Given: I am on the login page
            using (var logSession = new SerilogSession())
            using (var loginPage = new LoginPage(browser, TimeSpan.FromSeconds(5)))
            {
                SerilogLogger.LogInfo("Starting test UC2:");
                loginPage.Open();
                // When: I try to log in without password
                loginPage.FillUsername(username);
                loginPage.FillPassword(password);
                loginPage.ClearPassword();
                loginPage.Login();
                //Then: I see an error indicating that the password is required
                Assert.True(loginPage.IsErrorDisplayed("Password is required"));
                SerilogLogger.LogInfo($"Test UC2 with values {username}, {password} has passed.");
            }
        }
    }

    [Collection("Parallel Tests")]
    public class LoginSuccessTests
    {
        private readonly string browser = "Firefox";
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