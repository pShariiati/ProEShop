using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProEShop.Common.Attributes;

public class MakeTinyMceRequiredAttribute : BaseValidationAttribute, IClientModelValidator
{
    public MakeTinyMceRequiredAttribute()
    {
        ErrorMessage = "لطفا {0} را وارد نمایید";
    }

    protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
    {
        var displayName = validationContext.DisplayName;
        ErrorMessage = ErrorMessage.Replace("{0}", displayName);

        if (string.IsNullOrWhiteSpace(value?.ToString()))
        {
            return new ValidationResult(ErrorMessage);
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
        MergeAttribute(context.Attributes, "data-val-makeTinyMceRequired", ErrorMessage);
    }
}