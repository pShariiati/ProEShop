using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("CategoryBrands")]
public class CategoryBrand : IAuditableEntity
{
    #region Properties

    public long CategoryId { get; set; }

    public long BrandId { get; set; }

    #endregion

    #region Relations

    public Category Category { get; set; }
    public Brand Brand { get; set; }

    #endregion
}