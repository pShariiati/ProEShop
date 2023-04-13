using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEShop.Entities;

[Index(nameof(Category.Slug), IsUnique = true)]
[Index(nameof(Category.Title), IsUnique = true)]
[Table("Categories")]
public class Category : EntityBase, IAuditableEntity
{
    #region Properties

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Column(TypeName = "ntext")]
    public string Description { get; set; }

    [Required]
    [MaxLength(50)]
    public string Slug { get; set; }

    [MaxLength(50)]
    public string Picture { get; set; }

    public long? ParentId { get; set; }

    public bool ShowInMenus { get; set; }

    public bool CanAddFakeProduct { get; set; }

    /// <summary>
    /// اگر ترو باشد یعنی رنگ است
    /// فالس سایز
    /// نال یعنی بدون تنوع است
    /// </summary>
    public bool? IsVariantColor { get; set; }

    [MaxLength(1000)]
    public string ProductPageGuide { get; set; }

    /// <summary>
    /// اگر این دسته بندی براش تنوع اضافه شده باشه این پراپرتی ترو میشه
    /// </summary>
    public bool HasVariant { get; set; }

    #endregion

    #region Relations

    public Category Parent { get; set; }

    public ICollection<Category> Categories { get; set; }

    public ICollection<CategoryFeature> CategoryFeatures { get; set; }

    public ICollection<CategoryBrand> CategoryBrands { get; set; }
        = new List<CategoryBrand>();

    public ICollection<FeatureConstantValue> FeatureConstantValues { get; set; }

    public ICollection<ProductCategory> ProductCategories { get; set; }

    public ICollection<Product> Products { get; set; }

    public ICollection<CategoryVariant> CategoryVariants { get; set; }

    #endregion
}
