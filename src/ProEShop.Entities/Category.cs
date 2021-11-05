using System.ComponentModel.DataAnnotations;
using ProEShop.Entities.AuditableEntity;

namespace ProEShop.Entities
{
    public class Category : IAuditableEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Test { get; set; }
    }
}