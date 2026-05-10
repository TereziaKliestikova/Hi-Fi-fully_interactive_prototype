using System;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.ObjectRenderer;
using log4net.Repository;
using log4net.Repository.Hierarchy;
using log4net.Util;

namespace HIPA_BE.Logger
{
    public enum LoggingLevel { All, Debug, Info, Warning, Error }
    public class Logger

    {
        // For flexibility and use for higher abstraction in the future
        private static readonly Dictionary<LoggingLevel, log4net.Core.Level> _logLevelMap = new()
        {
            { LoggingLevel.All, log4net.Core.Level.All },
            { LoggingLevel.Debug, log4net.Core.Level.Debug },
            { LoggingLevel.Info, log4net.Core.Level.Info },
            { LoggingLevel.Warning, log4net.Core.Level.Warn },
            { LoggingLevel.Error, log4net.Core.Level.Error }
        };

        public void Setup(LoggingLevel level_in)
        {
            JsonLayout jsonLayout = new();

            RollingFileAppender fileAppender = new()
            {
                Layout = jsonLayout,
                AppendToFile = true,
                File = "./logs/hipa-be.log",
                RollingStyle = RollingFileAppender.RollingMode.Size,
                MaximumFileSize = "25MB",
                MaxSizeRollBackups = 2,
                LockingModel = new FileAppender.InterProcessLock()
            };
            fileAppender.ActivateOptions();

            ConsoleAppender consoleAppender = new()
            {
                Layout = jsonLayout
            };
            consoleAppender.ActivateOptions();

            ILoggerRepository repository = LogManager.GetRepository();
            Hierarchy hierarchy = (Hierarchy)repository;
            if (_logLevelMap.TryGetValue(level_in, out log4net.Core.Level? level_out))
            {
                ArgumentNullException.ThrowIfNull(level_out);
            }
            else
            {
                throw new Exception($"{level_in} is not a valid log level value.");
            }

            hierarchy.Root.Level = level_out;

            hierarchy.Root.RemoveAllAppenders();
            hierarchy.Root.AddAppender(fileAppender);
            hierarchy.Root.AddAppender(consoleAppender);

            hierarchy.Configured = true;

            Console.WriteLine("Logging setup ran.");
        }
    }
}
