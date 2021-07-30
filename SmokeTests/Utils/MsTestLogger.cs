using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SmokeTests.Utils
{
    public class MsTestLogger : ILogger
    {
        public MsTestLogger(TestContext testContext)
        {
            _testContext = testContext;
        }

        public IDisposable BeginScope<TState>(TState state) => default;

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var logRecord = $"[{DateTimeOffset.UtcNow:yyyy-MM-dd HH:mm:ss}] [{logLevel}] {formatter(state, exception)} {exception?.StackTrace ?? string.Empty}";

            _testContext.WriteLine(logRecord);
        }

        private readonly TestContext _testContext;
    }
}
