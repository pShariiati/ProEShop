using ProEShop.Entities.AuditableEntity;
using System.ComponentModel.DataAnnotations;

namespace ProEShop.Entities;

public class Category : EntityBase, IAuditableEntity
{
    [Required]
    [MaxLength(100)]
    public string? Title { get; set; }

    public string? Test { get; set; }
}
