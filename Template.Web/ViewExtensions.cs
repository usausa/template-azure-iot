namespace Template.Web;

using System.Globalization;

public static class ViewExtensions
{
    private static readonly TimeZoneInfo TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");

    private static DateTime ToLocal(this DateTime value) => TimeZoneInfo.ConvertTime(System.DateTime.SpecifyKind(value, DateTimeKind.Utc), TimeZone);

    public static string FormatCount(this int value) => value.ToString("#,0", CultureInfo.InvariantCulture);

    public static string FormatValue(this double value) => value.ToString("F2", CultureInfo.InvariantCulture);

    public static string DateTime(this DateTime value) => value.ToLocal().ToString("yyyy/MM/dd HH:mm", CultureInfo.InvariantCulture);
}
