using System.ComponentModel.DataAnnotations;

namespace ProEShop.ViewModels.GiftCards;

/// <summary>
/// ویوو مدل گرفتن کارت هدیه برای بررسی کردن در صفحه پرداخت
/// </summary>
public class GetGiftCardCodeDataViewModel
{
    public GetGiftCardCodeDataViewModel()
    {

    }

    public GetGiftCardCodeDataViewModel(int sumPriceOfCart, string giftCardCode)
    {
        SumPriceOfCart = sumPriceOfCart;
        GiftCardCode = giftCardCode;
    }

    [Range(1, int.MaxValue)]
    public int SumPriceOfCart { get; set; }

    [Required]
    [MaxLength(16)]
    public string GiftCardCode { get; set; }
}