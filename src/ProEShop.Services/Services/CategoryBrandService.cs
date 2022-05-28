using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.CategoryFeatures;

namespace ProEShop.Services.Services;

public class CategoryBrandService : GenericService<CategoryBrand>, ICategoryBrandService
{
    private readonly DbSet<CategoryBrand> _categoryBrands;

    public CategoryBrandService(
        IUnitOfWork uow)
        : base(uow)
    {
        _categoryBrands = uow.Set<CategoryBrand>();
    }

    public Task<bool> CheckCategoryBrand(long categoryId, long brandId)
    {
        return _categoryBrands.Where(x => x.CategoryId == categoryId)
            .AnyAsync(x => x.BrandId == brandId);
    }

    public async Task<(bool isSucessfull, byte value)> GetCommissionPercentage(long categoryId, long brandId)
    {
        var query = await _categoryBrands.Where(x => x.CategoryId == categoryId)
            .Where(x => x.BrandId == brandId)
            .SingleOrDefaultAsync();
        return (query != null, query?.CommissionPercentage ?? 0);
    }
}
