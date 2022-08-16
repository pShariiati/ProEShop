using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;

namespace ProEShop.Services.Services;

public class CategoryVariantService : CustomGenericService<CategoryVariant>, ICategoryVariantService
{
    private readonly DbSet<CategoryVariant> _categoryVariants;

    public CategoryVariantService(IUnitOfWork uow) : base(uow)
    {
        _categoryVariants = uow.Set<CategoryVariant>();
    }

    public Task<List<long>> GetCategoryVariants(long categoryId)
    {
        return _categoryVariants.Where(x => x.CategoryId == categoryId)
            .Select(x => x.VariantId)
            .ToListAsync();
    }
}