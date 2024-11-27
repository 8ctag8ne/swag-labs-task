using System.Collections.Concurrent;
using OpenQA.Selenium;

namespace SwagLabsTask.Utilities
{
    public sealed class WebDriverManager
    {
        private static readonly Lazy<WebDriverManager> _instance = 
            new Lazy<WebDriverManager>(() => new WebDriverManager(), LazyThreadSafetyMode.ExecutionAndPublication);
            
        private readonly ConcurrentDictionary<int, IWebDriver> _drivers = new ConcurrentDictionary<int, IWebDriver>();
        
        private WebDriverManager() { }

        public static WebDriverManager Instance => _instance.Value;

        public IWebDriver GetDriver(string browser)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            
            return _drivers.GetOrAdd(threadId, _ => 
            {
                var driver = Browser.CreateWebDriver(browser);
                SerilogLogger.LogInfo($"Created new WebDriver instance for thread {threadId}");
                return driver;
            });
        }

        public void ResetDriver()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            
            if (_drivers.TryRemove(threadId, out var driver))
            {
                try
                {
                    driver.Quit();
                    driver.Dispose();
                    SerilogLogger.LogInfo($"Driver cleaned up for thread {threadId}");
                }
                catch (Exception ex)
                {
                    SerilogLogger.LogError($"Error during driver cleanup for thread {threadId}: {ex.Message}");
                }
            }
        }

        public static void CleanupAllDrivers()
        {
            var instance = Instance;
            foreach (var threadId in instance._drivers.Keys.ToList())
            {
                if (instance._drivers.TryRemove(threadId, out var driver))
                {
                    try
                    {
                        driver.Quit();
                        driver.Dispose();
                        SerilogLogger.LogInfo($"Driver cleaned up for thread {threadId}");
                    }
                    catch (Exception ex)
                    {
                        SerilogLogger.LogError($"Error during driver cleanup for thread {threadId}: {ex.Message}");
                    }
                }
            }
        }
    }
}