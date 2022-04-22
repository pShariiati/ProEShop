using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProEShop.Common.Attributes;

public class FileRequiredAttribute : BaseValidationAttribute, IClientModelValidator
{
    public FileRequiredAttribute()
    {
        ErrorMessage = "لطفا {0} را وارد نمایید";
    }

    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        var displayName = validationContext.DisplayName;
        ErrorMessage = ErrorMessage.Replace("{0}", displayName);

        var files = value as List<IFormFile>;
        if (files == null || files.Count == 0)
        {
            return new ValidationResult(ErrorMessage);
        }
        foreach (var file in files)
        {
            if (file == null || file.Length == 0)
            {
                return new ValidationResult(ErrorMessage);
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

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-fileRequired", ErrorMessage);
    }
}
