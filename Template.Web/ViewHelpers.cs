namespace Template.Web;

using MudBlazor;

using Template.Models;

public static class ViewHelper
{
    public static string StatusColor(bool status)
    {
        return status ? Colors.Green.Accent4 : Colors.Grey.Default;
    }

    public static Color ValueColor(SensorEntity entity)
    {
        if (entity.IsWarning())
        {
            return Color.Warning;
        }
        if (entity.IsError())
        {
            return Color.Error;
        }
        return Color.Info;
    }
}
