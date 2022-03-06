using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProEShop.Common.Attributes;

public class MaxFileSizeAttribute : BaseValidationAttribute, IClientModelValidator
{
    private readonly int _maxFileSize;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="maxFileSize">By MG</param>
    /// <param name="displayName"></param>
    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize * 1024 * 1024;
        ErrorMessage = "اندازه {0} نباید بیشتر از {1} مگابایت باشد";
    }

    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        var displayName = validationContext.DisplayName;
        ErrorMessage = ErrorMessage.Replace("{0}", displayName);
        ErrorMessage = ErrorMessage.Replace("{1}", _maxFileSize.ToString());

        var file = value as IFormFile;
        if (file != null && file.Length > 0)
        {
            if (file.Length > _maxFileSize)
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
        ErrorMessage = ErrorMessage.Replace("{1}", _maxFileSize.ToString());

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-maxFileSize", ErrorMessage);
        MergeAttribute(context.Attributes, "data-val-maxsize", _maxFileSize.ToString());
    }
}
