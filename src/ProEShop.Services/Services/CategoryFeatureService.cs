using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.CategoryFeatures;

namespace ProEShop.Services.Services;

public class CategoryFeatureService : CustomGenericService<CategoryFeature>, ICategoryFeatureService
{
    private readonly DbSet<CategoryFeature> _categoryFeatures;
    private readonly IMapper _mapper;

    public CategoryFeatureService(
        IUnitOfWork uow,
        IMapper mapper) : base(uow)
    {
        _mapper = mapper;
        _categoryFeatures = uow.Set<CategoryFeature>();
    }
    public async Task<CategoryFeature> GetCategoryFeature(long categoryId, long featureId)
    {
        return await _categoryFeatures.Where(x => x.CategoryId == categoryId)
            .SingleOrDefaultAsync(x => x.FeatureId == featureId);
    }

    public Task<List<CategoryFeatureForCreateProductViewModel>> GetCategoryFeatures(long categoryId)
    {
        return _mapper.ProjectTo<CategoryFeatureForCreateProductViewModel>(
            _categoryFeatures.Where(x => x.CategoryId == categoryId)
        ).ToListAsync();
    }

    public Task<bool> CheckCategoryFeature(long categoryId, long featureId)
    {
        return _categoryFeatures.Where(x => x.CategoryId == categoryId)
            .AnyAsync(x => x.FeatureId == featureId);
    }

    public async Task<bool> CheckCategoryFeaturesCount(long categoryId, List<long> featureIds)
    {
        var featuresCount = await _categoryFeatures
            .Where(x => x.CategoryId == categoryId)
            .CountAsync(x => featureIds.Contains(x.FeatureId));
        return featuresCount == featureIds.Count();
    }
}
