namespace ProEShop.Common.Helpers;

public class ConvertDateForCreateSeller
{
    public ConvertDateForCreateSeller(bool isOk, bool isRangeOk = default, DateTime convertedDateTime = default)
    {
        IsOk = isOk;
        IsRangeOk = isRangeOk;
        ConvertedDateTime = convertedDateTime;
    }

    public bool IsOk { get; set; }

    public bool IsRangeOk { get; set; }

    public DateTime ConvertedDateTime { get; set; }
}