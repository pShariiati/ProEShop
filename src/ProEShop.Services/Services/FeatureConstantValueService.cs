using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.Services.Services;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.FeatureConstantValues;
using SortingBrands = ProEShop.ViewModels.FeatureConstantValues.SortingBrands;

namespace ProEShop.Services.Services;

public class FeatureConstantValueService : GenericService<FeatureConstantValue>, IFeatureConstantValueService
{
    private readonly DbSet<FeatureConstantValue> _featureConstantValues;
    private readonly IMapper _mapper;

    public FeatureConstantValueService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _featureConstantValues = uow.Set<FeatureConstantValue>();
    }

    public async Task<ShowFeatureConstantValuesViewModel> GetFeatureConstantValues(ShowFeatureConstantValuesViewModel model)
    {
        var featureConstantValues = _featureConstantValues.AsNoTracking().AsQueryable();

        #region Search

        var searchedFeatureId = model.SearchFeatureConstantValues.FeatureId;
        if (searchedFeatureId != 0)
        {
            featureConstantValues = featureConstantValues.Where(x => x.FeatureId == searchedFeatureId);
        }

        featureConstantValues = ExpressionHelpers.CreateSearchExpressions(featureConstantValues, model.SearchFeatureConstantValues);

        #endregion

        #region OrderBy

        if (model.SearchFeatureConstantValues.Sorting == SortingBrands.FeatureTitle)
        {
            if (model.SearchFeatureConstantValues.SortingOrder == SortingOrder.Asc)
            {
                featureConstantValues = featureConstantValues.OrderBy(x => x.Feature.Title);
            }
            else
            {
                featureConstantValues = featureConstantValues.OrderByDescending(x => x.Feature.Title);
            }
        }
        else
        {
            featureConstantValues = featureConstantValues.CreateOrderByExpression(model.SearchFeatureConstantValues.Sorting.ToString(),
                model.SearchFeatureConstantValues.SortingOrder.ToString());
        }

        #endregion

        var paginationResult = await GenericPaginationAsync(featureConstantValues, model.Pagination);

        return new()
        {
            FeatureConstantValues = await _mapper.ProjectTo<ShowFeatureConstantValueViewModel>(
                paginationResult.Query
            ).ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }

    public Task<List<ShowCategoryFeatureConstantValueViewModel>> GetFeatureConstantValuesByCategoryId(long categoryId)
    {
        return _mapper.ProjectTo<ShowCategoryFeatureConstantValueViewModel>(
            _featureConstantValues.Where(x => x.CategoryId == categoryId)
        ).ToListAsync();
    }

    public Task<bool> CheckNonConstantValue(long categoryId, List<long> featureIds)
    {
        return _featureConstantValues
            .Where(x => x.CategoryId == categoryId)
            .AnyAsync(x => featureIds.Contains(x.FeatureId));
    }

    public async Task<bool> CheckConstantValue(long categoryId, List<long> featureConstantValueIds)
    {
        var featuresCount = await _featureConstantValues
            .Where(x => x.CategoryId == categoryId)
            .GroupBy(x=>x.FeatureId)
            .CountAsync(x => featureConstantValueIds.Contains(x.Key));
        return featuresCount == featureConstantValueIds.Count;
    }
}