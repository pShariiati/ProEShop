using Microsoft.EntityFrameworkCore;
using ProEShop.Entities;

namespace ProEShop.DataLayer;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }
}
