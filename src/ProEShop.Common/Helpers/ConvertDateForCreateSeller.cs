namespace ProEShop.Common.Helpers;

public class ConvertDateForCreateSeller
{
    public ConvertDateForCreateSeller(bool isOk, bool isGreaterThan18 = default, DateTime convertedDateTime = default)
    {
        IsOk = isOk;
        IsGreaterThan18 = isGreaterThan18;
        ConvertedDateTime = convertedDateTime;
    }

    public bool IsOk { get; set; }

    public bool IsGreaterThan18 { get; set; }

    public DateTime ConvertedDateTime { get; set; }
}