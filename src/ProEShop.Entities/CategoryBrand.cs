using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("CategoryBrands")]
[Index(nameof(CategoryBrand.CategoryId), nameof(CategoryBrand.BrandId),
    IsUnique = true)]
public class CategoryBrand : EntityBase, IAuditableEntity
{
    #region Properties

    public long CategoryId { get; set; }

    public long BrandId { get; set; }

    public byte CommissionPercentage { get; set; }

    #endregion

    #region Relations

    public Category Category { get; set; }
    public Brand Brand { get; set; }

    #endregion
}