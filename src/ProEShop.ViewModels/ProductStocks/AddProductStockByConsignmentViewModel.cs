using System.ComponentModel.DataAnnotations;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.ProductStocks;

public class AddProductStockByConsignmentViewModel
{
    [Display(Name = "شناسه محموله")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Range(1, long.MaxValue, ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public long ConsignmentId { get; set; }

    [Display(Name = "شناسه تنوع محصول")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Range(1, long.MaxValue, ErrorMessage = AttributesErrorMessages.RegularExpressionMessage)]
    public long ProductVariantId { get; set; }

    [Display(Name = "تعداد")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [Range(1, 100000, ErrorMessage = AttributesErrorMessages.RangeMessage)]
    public int Count { get; set; }
}