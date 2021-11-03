using System.ComponentModel.DataAnnotations;

namespace ProEShop.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
    }
}