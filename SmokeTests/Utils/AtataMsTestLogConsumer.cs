using Atata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using AtataLogLevel = Atata.LogLevel;
using MsLogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace SmokeTests.Utils
{
    /// <summary>
    /// Log consumer to hook ILogger into Atata's log framework making all logs consistent
    /// </summary>
    public class AtataMsTestLogConsumer : ILogConsumer
    {
        public AtataMsTestLogConsumer(ILogger logger)
        {
            _logger = logger;
        }

        public void Log(LogEventInfo eventInfo)
        {
            var level = LazyMappedLogLevels.Value[eventInfo.Level];
            var message = eventInfo.Message;
            var ex = eventInfo.Exception;

            _logger.Log(level, ex, message);
        }

        private static readonly Lazy<Dictionary<AtataLogLevel, MsLogLevel>> LazyMappedLogLevels =
            new Lazy<Dictionary<AtataLogLevel, MsLogLevel>>(MappedLogLevels);

        private static Dictionary<AtataLogLevel, MsLogLevel> MappedLogLevels()
        {
            return new Dictionary<AtataLogLevel, MsLogLevel>
            {
                { AtataLogLevel.Trace, MsLogLevel.Trace },
                { AtataLogLevel.Debug, MsLogLevel.Debug },
                { AtataLogLevel.Info, MsLogLevel.Information },
                { AtataLogLevel.Warn, MsLogLevel.Warning },
                { AtataLogLevel.Error, MsLogLevel.Error },
                { AtataLogLevel.Fatal, MsLogLevel.Critical }
            };
        }

        private readonly ILogger _logger;
    }
}
