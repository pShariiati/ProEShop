using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProEShop.Common.Attributes;

public class AllowExtensionsAttribute : BaseValidationAttribute, IClientModelValidator
{
    private readonly string[] _allowExtensions;
    private readonly string[] _allowContentTypes;
    private readonly string _errorMessage;

    public AllowExtensionsAttribute(string displayName, string[] allowExtensions, string[] allowContentTypes)
    {
        _allowExtensions = allowExtensions;
        _errorMessage = $"فرمت های مجاز برای {displayName} ";

        foreach (var allowExtension in allowExtensions)
        {
            _errorMessage += $"{allowExtension}, ";
        }

        _allowContentTypes = allowContentTypes;
        _errorMessage = _errorMessage.Trim(' ');
        _errorMessage = _errorMessage.Trim(',');
    }

    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null && file.Length > 0)
        {
            if (!_allowExtensions.Contains(file.ContentType))
            {
                return new ValidationResult(_errorMessage);
            }
        }
        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-allowExtensions", _errorMessage);
        MergeAttribute(context.Attributes, "data-val-whitelistextensions",
            string.Join(",", _allowExtensions));
    }
}
