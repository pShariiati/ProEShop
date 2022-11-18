using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
