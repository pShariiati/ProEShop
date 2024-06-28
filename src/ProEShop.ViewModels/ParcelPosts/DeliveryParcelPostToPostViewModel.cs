using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.ParcelPosts;

public class DeliveryParcelPostToPostViewModel
{
    [HiddenInput]
    public long Id { get; set; }

    [Display(Name = "کد رهگیری اداره پست")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(30, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string PostTrackingCode { get; set; }
}
