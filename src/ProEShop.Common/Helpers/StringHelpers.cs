using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProEShop.Common.Helpers;

public static class StringHelpers
{
    public static bool IsEmail(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;
        return input.Contains('@');
    }

    public static string GenerateGuid() => Guid.NewGuid().ToString("N");

    public static List<string> AddDuplicateErrors<T>(this List<string> duplicateColumns)
    {
        var result = new List<string>();
        foreach (var duplicateColumn in duplicateColumns)
        {
            var columnDisplayName = typeof(T).GetProperty(duplicateColumn)!
                .GetCustomAttribute<DisplayAttribute>()!.Name;
            result.Add($"این {columnDisplayName} قبلا در سیستم ثبت شده است");
        }
        return result;
    }
}
