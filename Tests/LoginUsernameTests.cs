using SwagLabsTask.PageObjects;
using Xunit;
using SwagLabsTask.Utilities;
namespace SwagLabsTestTask
{
    public class LoginUsernameTests
    {
        private readonly string browser = "chrome";
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
}