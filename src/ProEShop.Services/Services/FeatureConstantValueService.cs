﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.Services.Services;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.FeatureConstantValues;

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

        featureConstantValues = ExpressionHelpers.CreateSearchExpressions(featureConstantValues, model.SearchFeatureConstantValues);

        #endregion

        #region OrderBy

        if (false)
        {

        }
        else if (false)
        {
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
}