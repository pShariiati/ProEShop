namespace ProEShop.ViewModels.Orders;

/// <summary>
/// دیتا های صفحه تایید کردن پرداختی
/// </summary>
public class VerifyPageDataViewModel
{
    public bool IsPay { get; set; }

    public string BankTransactionCode { get; set; }

    public long OrderNumber { get; set; }
}