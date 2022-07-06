using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Attributes;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.Consignments;

public class AddDescriptionForConsignmentViewModel
{
    [Display(Name = "شناسه محموله")]
    [HiddenInput]
    [Range(1, long.MaxValue, ErrorMessage = AttributesErrorMessages.RangeMessage)]
    public long ConsignmentId { get; set; }

    [Display(Name = "توضیحات محموله")]
    [MakeTinyMceRequired]
    public string Description { get; set; }
}