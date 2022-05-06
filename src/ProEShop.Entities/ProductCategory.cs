using System.ComponentModel.DataAnnotations.Schema;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities;

[Table("ProductCategories")]
public class ProductCategory : IAuditableEntity
{
    #region Properties

    public long ProductId { get; set; }

    public long CategoryId { get; set; }

    #endregion

    #region Relations

    public Product Product { get; set; }

    public Category Category { get; set; }

    #endregion
}