using System.ComponentModel.DataAnnotations;

namespace ProEShop.Entities.Enums;

public enum OrderStatus : byte
{
    [Display(Name = "در انتظار پرداخت")]
    WaitingForPaying,

    [Display(Name = "در حال پردازش")]
    Processing,

    [Display(Name = "پردازش انبار")]
    InventoryProcessing,

    [Display(Name = "بخشی از مرسوله ها در پست")]
    SomeParcelsDeliveredToPost,

    [Display(Name = "تمام مرسوله ها در پست")]
    CompletelyParcelsDeliveredToPost,

    [Display(Name = "تحویل شده")]
    DeliveredToClient
}

public enum PaymentGateway : byte
{
    [Display(Name = "زرین پال")]
    Zarinpal,

    [Display(Name = "به پرداخت ملت")]
    Mellat,

    [Display(Name = "درگاه مجازی تست")]
    ParbadVirtual,
}