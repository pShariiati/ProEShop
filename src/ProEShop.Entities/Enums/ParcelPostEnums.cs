using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEShop.Entities.Enums;

public enum ParcelPostStatus : byte
{
    [Display(Name = "در انتظار پرداخت")]
    WaitingForPaying,

    [Display(Name = "در حال پردازش")]
    Processing,

    [Display(Name = "پردازش انبار")]
    InventoryProcessing,

    [Display(Name = "تحویل به پست")]
    DeliveredToPost,

    [Display(Name = "تحویل شده")]
    DeliveredToClient
}