namespace ProEShop.ViewModels.ProductVariants;

public class ShowProductVariantInCreateConsignmentViewModel
{
    public long ProductId { get; set; }

    public string ProductPersianTitle { get; set; }

    public int Price { get; set; }

    public int VariantCode { get; set; }

    public string VariantValue { get; set; }

    public string VariantColorCode { get; set; }

    public bool VariantIsColor { get; set; }

    public string GuaranteeFullTitle { get; set; }
}