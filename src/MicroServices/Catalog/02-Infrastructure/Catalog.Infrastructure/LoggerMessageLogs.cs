using Microsoft.Extensions.Logging;

namespace Catalog.ApplicationServices;

public static partial class LoggerMessageLogs
{
    [LoggerMessage(EventId = 2000, Level = LogLevel.Error, Message = "Entity: {EntityName} with id: {Id} Not Found!-{Date}")]
    private static partial void LogNotFoundException(ILogger logger, object id, string entityName ,DateTime date);

    public static void LogNotFoundException(this  ILogger logger, object id, string entityName)
    {
        var dateTime = DateTime.UtcNow;
        LogNotFoundException(logger, id, entityName, dateTime);
    }
   
}
