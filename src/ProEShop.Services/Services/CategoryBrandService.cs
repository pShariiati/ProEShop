using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.CategoryFeatures;

namespace ProEShop.Services.Services;

public class CategoryBrandService : CustomGenericService<CategoryBrand>, ICategoryBrandService
{
    private readonly DbSet<CategoryBrand> _categoryBrands;

    public CategoryBrandService(
        IUnitOfWork uow,
        IMapper mapper) : base(uow)
    {
        _categoryBrands = uow.Set<CategoryBrand>();
    }

    public Task<bool> CheckCategoryBrand(long categoryId, long brandId)
    {
        return _categoryBrands.Where(x => x.CategoryId == categoryId)
            .AnyAsync(x => x.BrandId == brandId);
    }
}
