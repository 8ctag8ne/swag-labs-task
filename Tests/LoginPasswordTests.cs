using SwagLabsTask.PageObjects;
using Xunit;
using SwagLabsTask.Utilities;

namespace SwagLabsTestTask
{   
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
}