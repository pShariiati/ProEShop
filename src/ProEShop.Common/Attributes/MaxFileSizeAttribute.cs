using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProEShop.Common.Attributes;

public class MaxFileSizeAttribute : BaseValidationAttribute, IClientModelValidator
{
    private readonly int _maxFileSize;
    private readonly string _errorMessage;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="maxFileSize">By MG</param>
    /// <param name="displayName"></param>
    public MaxFileSizeAttribute(string displayName, int maxFileSize)
    {
        _maxFileSize = maxFileSize * 1024 * 1024;
        _errorMessage = $"اندازه {displayName} نباید بیشتر از {maxFileSize} مگابایت باشد";
    }

    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file != null && file.Length > 0)
        {
            if (file.Length > _maxFileSize)
            {
                return new ValidationResult(_errorMessage);
            }
        }

        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-maxFileSize", _errorMessage);
        MergeAttribute(context.Attributes, "data-val-maxsize", _maxFileSize.ToString());
    }
}
