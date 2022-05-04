using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProEShop.Common.Attributes;

public class MaxFileSizeAttribute : BaseValidationAttribute, IClientModelValidator
{
    private readonly bool _multiplePictures;
    private readonly int _maxFileSize;
    private readonly int _maxFileSizeInBytes;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="maxFileSize">By MG</param>
    /// <param name="displayName"></param>
    public MaxFileSizeAttribute(int maxFileSize, bool multiplePictures = false)
    {
        _multiplePictures = multiplePictures;
        _maxFileSize = maxFileSize;
        _maxFileSizeInBytes = maxFileSize * 1024 * 1024;
        ErrorMessage = "اندازه {0} نباید بیشتر از {1} مگابایت باشد";
    }

    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        var displayName = validationContext.DisplayName;
        ErrorMessage = ErrorMessage.Replace("{0}", displayName);
        ErrorMessage = ErrorMessage.Replace("{1}", _maxFileSize.ToString());

        if (_multiplePictures)
        {
            ErrorMessage = ErrorMessage.Replace("باشد", "باشند");
        }

        var files = value as List<IFormFile>;

        if (files is { Count: > 0 })
        {
            for (int counter = 0; counter < files.Count; counter++)
            {
                var currentFile = files[counter];
                if (currentFile is { Length: > 0 })
                {
                    if (currentFile.Length > _maxFileSizeInBytes)
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
        ErrorMessage = ErrorMessage.Replace("{1}", _maxFileSize.ToString());

        if (_multiplePictures)
        {
            ErrorMessage = ErrorMessage.Replace("باشد", "باشند");
        }

        MergeAttribute(context.Attributes, "data-val", "true");
        MergeAttribute(context.Attributes, "data-val-maxFileSize", ErrorMessage);
        MergeAttribute(context.Attributes, "data-val-maxsize", _maxFileSizeInBytes.ToString());
    }
}
