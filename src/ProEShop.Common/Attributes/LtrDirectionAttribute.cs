using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProEShop.Common.Attributes;

public class LtrDirectionAttribute : BaseValidationAttribute, IClientModelValidator
{
    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        return ValidationResult.Success;
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-ltrDirection", "true");
    }
}
