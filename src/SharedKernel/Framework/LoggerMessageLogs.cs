using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public static partial class LoggerMessageLogs
    {
        [LoggerMessage(EventId = 100, Level = LogLevel.Information, Message = "Command {CommandName} Started handeling ... Start Time : {Date}")]
        private static partial void LogCommandsBefore(ILogger logger, string commandName, DateTime date);


        public static void LogCommandsBefore(this ILogger logger, string commandName)
        {
            var dateTime = DateTime.UtcNow;
            LogCommandsBefore(logger, commandName, dateTime);
        }

        [LoggerMessage(EventId = 101, Level = LogLevel.Critical, Message = "Command {CommandName} Ended handeling ... End Time : {Date} and  took: {Elapsed} ms")]
        private static partial void LogCommandsAfterCritical(ILogger logger, string commandName, DateTime date, long elapsed);

        public static void LogCommandsAfterCritical(this ILogger logger, string commandName, long elapsed)
        {
            var dateTime = DateTime.UtcNow;
            LogCommandsAfterCritical(logger, commandName, dateTime, elapsed);
        }
        
        [LoggerMessage(EventId = 102, Level = LogLevel.Information, Message = "Command {CommandName} Ended handeling ... End Time : {Date} and  took: {Elapsed} ms")]
        private static partial void LogCommandsAfter(ILogger logger, string commandName, DateTime date, long elapsed);

        public static void LogCommandsAfter(this ILogger logger, string commandName, long elapsed)
        {
            var dateTime = DateTime.UtcNow;
            LogCommandsAfter(logger, commandName, dateTime, elapsed);
        }

    }

}
