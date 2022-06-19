using System.ComponentModel.DataAnnotations;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.Consignments;

public class CreateConsignmentViewModel
{
    [Display(Name = "تاریخ تحویل")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    public string DeliveryDate { get; set; }
}