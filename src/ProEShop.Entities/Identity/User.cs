using Microsoft.AspNetCore.Identity;
using ProEShop.Entities.AuditableEntity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEShop.Entities.Identity;

public class User : IdentityUser<long>, IAuditableEntity
{
    [MaxLength(200)]
    public string? FirstName { get; set; }

    [MaxLength(200)]
    public string? LastName { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";

    public bool IsActive { get; set; } = true;

    public DateTime CreatedDateTime { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Avatar { get; set; }

    public virtual ICollection<UserClaim>? UserClaims { get; set; }
    public virtual ICollection<UserLogin>? UserLogins { get; set; }
    public virtual ICollection<UserRole>? UserRoles { get; set; }
    public virtual ICollection<UserToken>? UserTokens { get; set; }
}
