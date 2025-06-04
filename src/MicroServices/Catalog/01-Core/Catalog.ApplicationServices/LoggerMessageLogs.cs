using Microsoft.Extensions.Logging;

namespace Catalog.ApplicationServices;

public static partial class LoggerMessageLogs
{
    [LoggerMessage(EventId = 1000, Level = LogLevel.Information, Message = "Command Started handeling ... Start Time : {Date}")]
    private static partial void LogCommandsBefore(ILogger logger, DateTime date);


    public static void LogCommandsBefore(this ILogger logger)
    {
        var dateTime = DateTime.UtcNow;
        LogCommandsBefore(logger, dateTime);
    }

    [LoggerMessage(EventId = 1001, Level = LogLevel.Critical, Message = "Command Ended handeling ... End Time : {Date} and  took: {Elapsed}")]
    private static partial void LogCommandsAfterCritical(ILogger logger, DateTime date, TimeSpan elapsed);

    public static void LogCommandsAfterCritical(this ILogger logger, TimeSpan elapsed)
    {
        var dateTime = DateTime.UtcNow;
        LogCommandsAfterCritical(logger, dateTime, elapsed);
    }

    [LoggerMessage(EventId = 100, Level = LogLevel.Information, Message = "Book Added in date: {Date} with id: {Id}")]
    private static partial void LogAddBook(ILogger logger, DateTime date, long id);

    public static void LogAddBook(this ILogger logger, long id)
    {
        var dateTime = DateTime.UtcNow;
        LogAddBook(logger, dateTime, id);
    }
}
