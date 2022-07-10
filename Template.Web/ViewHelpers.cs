namespace Template.Web;

using MudBlazor;

public static class ViewHelper
{
    public static string StatusColor(bool status)
    {
        return status ? Colors.Green.Accent4 : Colors.Grey.Default;
    }
}
