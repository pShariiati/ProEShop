using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProEShop.Common.Constants;

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

    public static List<string> SetDuplicateColumnsErrorMessages<T>(this List<string> duplicateColumns)
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

    public static void CheckStringInputs<T>(this ModelStateDictionary modelState, List<string> properties, T model)
    {
        foreach (var property in properties)
        {
            var currentProperty = typeof(T).GetProperty(property);
            var propertyValue = currentProperty.GetValue(model);
            if (string.IsNullOrWhiteSpace(propertyValue?.ToString()))
            {
                var propertyDisplayName = currentProperty!
                    .GetCustomAttribute<DisplayAttribute>()!.Name;
                modelState.AddModelError(property,
                    AttributesErrorMessages.RequiredMessage.Replace("{0}", propertyDisplayName));
            }
        }
    }

    public static string GenerateFileName(this IFormFile file)
    {
        return GenerateGuid() + Path.GetExtension(file.FileName);
    }

    public static string ToShowGuaranteeFullTitle(this string input)
    {
        if (input.Contains("0 ماهه"))
        {
            return "گارانتی اصالت و سلامت فیزیکی کالا";
        }
        return input;
    }
}
