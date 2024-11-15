using Serilog;

namespace SwagLabsTask.Utilities
{
    public static class SerilogLogger
    {
        static SerilogLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/test_logs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public static void Initialize()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/test_logs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public static void LogInfo(string message)
        {
            Log.Information(message);
        }

        public static void LogWarning(string message)
        {
            Log.Warning(message);
        }

        public static void LogError(string message, Exception ex = null)
        {
            if (ex != null)
            {
                Log.Error(ex, message);
            }
            else
            {
                Log.Error(message);
            }
        }
        public static void Separate()
        {
            string sep = string.Empty;
            for(int i = 0; i < 100; i++)
            {
                sep+="#";
            }
            LogInfo(sep);
        }

        public static void CloseAndFlush()
        {
            Log.CloseAndFlush();
        }
    }
}
