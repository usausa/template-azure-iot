namespace Template.Functions;

internal static partial class Log
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Event received: message=[{json}]")]
    public static partial void InfoEventReceived(this ILogger logger, string json);
}
