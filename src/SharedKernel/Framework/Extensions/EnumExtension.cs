using System.ComponentModel.DataAnnotations;

namespace Framework.Extensions;

public static class EnumExtension
{
    public static string GetDisplayName(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attr = field?.GetCustomAttributes(typeof(DisplayAttribute), false)
                         .FirstOrDefault() as DisplayAttribute;

        return attr?.GetName() ?? value.ToString();
    }
}
