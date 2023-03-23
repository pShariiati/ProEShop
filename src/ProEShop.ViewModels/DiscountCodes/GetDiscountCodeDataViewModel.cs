using System.ComponentModel.DataAnnotations;

namespace ProEShop.ViewModels.DiscountCodes;

/// <summary>
/// ویوو مدل گرفتن کد تخفیف برای بررسی کردن در صفحه پرداخت
/// </summary>
public class GetDiscountCodeDataViewModel
{
    public GetDiscountCodeDataViewModel()
    {
        
    }

    public GetDiscountCodeDataViewModel(int sumPriceOfCart, string discountCode)
    {
        SumPriceOfCart = sumPriceOfCart;
        DiscountCode = discountCode;
    }

    [Range(1, int.MaxValue)]
    public int SumPriceOfCart { get; set; }

    [Required]
    [MaxLength(100)]
    public string DiscountCode { get; set; }
}