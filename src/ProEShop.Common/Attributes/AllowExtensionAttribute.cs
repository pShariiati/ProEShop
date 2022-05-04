using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProEShop.Common.Attributes;

public class AllowExtensionsAttribute : BaseValidationAttribute, IClientModelValidator
{
    private readonly string[] _allowContentTypes;
    private readonly bool _multipleFiles;

    public AllowExtensionsAttribute(string[] allowExtensions, string[] allowContentTypes,
        bool multipleFiles = false)
    {
        ErrorMessage = "تنها فرمت های مجاز برای {0}: ";

        foreach (var allowExtension in allowExtensions)
        {
            ErrorMessage += $"{allowExtension}, ";
        }

        _allowContentTypes = allowContentTypes;
        _multipleFiles = multipleFiles;
        ErrorMessage = ErrorMessage.Trim(' ');
        ErrorMessage = ErrorMessage.Trim(',');
    }

    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        var displayName = validationContext.DisplayName;
        ErrorMessage = ErrorMessage.Replace("{0}", displayName);

        var files = value as List<IFormFile>;
        if (files is { Count: > 0 })
        {
            for (int counter = 0; counter < files.Count; counter++)
            {
                var currentFile = files[counter];
                if (currentFile is { Length: > 0 })
                {
                    if (!_allowContentTypes.Contains(currentFile.ContentType))
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }
        }
        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        var displayName = context.ModelMetadata.ContainerMetadata
            .ModelType.GetProperty(context.ModelMetadata.PropertyName)
            .GetCustomAttributes(typeof(DisplayAttribute), false)
            .Cast<DisplayAttribute>()
            .FirstOrDefault()?.Name;
        ErrorMessage = ErrorMessage.Replace("{0}", displayName);

        if (_multipleFiles)
        {
            ErrorMessage = ErrorMessage.Replace("باشد", "باشند");
        }

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-allowExtensions", ErrorMessage);
        MergeAttribute(context.Attributes, "data-val-whitelistextensions",
            string.Join(",", _allowContentTypes));
    }
}
