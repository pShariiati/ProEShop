using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProEShop.Common.Attributes;

public class FileRequiredAttribute : BaseValidationAttribute, IClientModelValidator
{
    private readonly string _errorMessage;

    public FileRequiredAttribute(string displayName)
    {
        _errorMessage = $"لطفا {displayName} را وارد نمایید";
    }

    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file == null || file.Length == 0)
        {
            return new ValidationResult(_errorMessage);
        }
        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-fileRequired", _errorMessage);
    }
}
