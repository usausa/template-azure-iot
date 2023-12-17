namespace Template.Models;

using Smart.Data.Accessor.Attributes;

public sealed class SensorEntity
{
    [Key]
    public Guid Id { get; set; }

    public double Value { get; set; }

    public DateTimeOffset Timestamp { get; set; }
}

public static class SensorEntityExtensions
{
    public static bool IsRunning(this SensorEntity value) => value.Timestamp > DateTimeOffset.UtcNow.AddMinutes(-10);

    public static bool IsWarning(this SensorEntity value) => value.Value >= 75 && value.Value < 90;

    public static bool IsError(this SensorEntity value) => value.Value >= 90;
}
