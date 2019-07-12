using UnityEngine;

public static class Extensions
{
    public static string PaingToHTML(this string s, Color color)
    {
        return new System.Text.StringBuilder("<color=#")
            .Append(ColorUtility.ToHtmlStringRGB(color))
            .Append(">")
            .Append(s)
            .Append("</color>")
            .ToString();
    }
}
