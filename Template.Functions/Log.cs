namespace Template.Functions;

#pragma warning disable SYSLIB1006
public static partial class Log
{
    [LoggerMessage(Level = LogLevel.Information, Message = "Event received: message=[{json}]")]
    public static partial void InfoEventReceived(this ILogger logger, string json);
}
#pragma warning restore SYSLIB1006
