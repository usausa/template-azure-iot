namespace Template.Models;

using Smart.Data.Accessor.Attributes;

public class SensorEntity
{
    [Key]
    public Guid Id { get; set; }

    public double Value { get; set; }

    public DateTime Timestamp { get; set; }
}

public static class SensorEntityExtensions
{
    public static bool IsRunning(this SensorEntity value) => value.Timestamp > DateTime.UtcNow.AddMilliseconds(-30);
}