using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ProEShop.Common.Helpers;

public static class EnumExtensions
{
    public static string GetEnumDisplayName(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName();
    }
}