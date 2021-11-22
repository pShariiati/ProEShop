using Microsoft.EntityFrameworkCore;
using ProEShop.Entities.AuditableEntity;
using System.ComponentModel.DataAnnotations;

namespace ProEShop.Entities;

[Index(nameof(Category.Slug), IsUnique = true)]
[Index(nameof(Category.Title), IsUnique = true)]
public class Category : EntityBase, IAuditableEntity
{
    #region Properties

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [MaxLength(2000)]
    public string Description { get; set; }

    [Required]
    [MaxLength(130)]
    public string Slug { get; set; }

    [MaxLength(50)]
    public string Picture { get; set; }

    public long? ParentId { get; set; }

    public bool ShowInMenus { get; set; }

    #endregion

    #region Relations

    public Category Parent { get; set; }

    public ICollection<Category> Categories { get; set; }
    public ICollection<Product> Products { get; set; }

    #endregion
}
