namespace Template.Web;

using System.Globalization;

public static class ViewExtensions
{
    private static readonly TimeZoneInfo TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

    private static DateTimeOffset ToLocal(this DateTimeOffset value) => TimeZoneInfo.ConvertTime(value, TimeZone);

    public static string FormatCount(this int value) => value.ToString("#,0", CultureInfo.InvariantCulture);

    public static string FormatValue(this double value) => value.ToString("F2", CultureInfo.InvariantCulture);

    public static string DateTime(this DateTimeOffset value) => value.ToLocal().ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
}
