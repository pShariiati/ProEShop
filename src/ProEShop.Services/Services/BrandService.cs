using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Categories;

namespace ProEShop.Services.Services;

public class BrandService : GenericService<Brand>, IBrandService
{
    private readonly DbSet<Brand> _brands;
    private readonly IMapper _mapper;

    public BrandService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _brands = uow.Set<Brand>();
    }

    public async Task<ShowBrandsViewModel> GetBrands(ShowBrandsViewModel model)
    {
        var brands = _brands.AsNoTracking().AsQueryable();

        #region Search

        brands = ExpressionHelpers.CreateSearchExpressions(brands, model.SearchBrands);

        #endregion

        #region OrderBy

        if (model.SearchBrands.Sorting == SortingBrands.BrandLinkEn)
        {
            if (model.SearchBrands.SortingOrder == SortingOrder.Asc)
            {
                brands = brands.OrderBy(x => x.BrandLinkEn.Substring(
                    x.BrandLinkEn.StartsWith("https://") ? 8 : 7
                ));
            }
            else
            {
                brands = brands.OrderByDescending(x => x.BrandLinkEn.Substring(
                    x.BrandLinkEn.StartsWith("https://") ? 8 : 7
                ));
            }
        }
        else if (model.SearchBrands.Sorting == SortingBrands.JudiciaryLink)
        {
            if (model.SearchBrands.SortingOrder == SortingOrder.Asc)
            {
                brands = brands.OrderBy(x => x.JudiciaryLink.Substring(
                    x.JudiciaryLink.StartsWith("https://") ? 8 : 7
                ));
            }
            else
            {
                brands = brands.OrderByDescending(x => x.JudiciaryLink.Substring(
                    x.JudiciaryLink.StartsWith("https://") ? 8 : 7
                ));
            }
        }
        else
        {
            brands = brands.CreateOrderByExpression(model.SearchBrands.Sorting.ToString(),
                model.SearchBrands.SortingOrder.ToString());
        }

        #endregion

        var paginationResult = await GenericPaginationAsync(brands, model.Pagination);

        return new()
        {
            Brands = await _mapper.ProjectTo<ShowBrandViewModel>(
                    paginationResult.Query
                ).ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }

    public Task<EditBrandViewMode> GetForEdit(long id)
    {
        return _mapper.ProjectTo<EditBrandViewMode>(
                _brands
            ).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<string>> AutocompleteSearch(string term)
    {
        return await _brands
            .Where(x => x.TitleFa.Contains(term) || x.TitleEn.Contains(term))
            .OrderBy(x => x.Id)
            .Take(20)
            .Select(x => x.TitleFa + " " + x.TitleEn)
            .ToListAsync();
    }

    public Task<Dictionary<long, string>> GetBrandsByFullTitle(List<string> brandTitles)
    {
        return _brands
            .Where(x => brandTitles.Contains(x.TitleFa + " " + x.TitleEn))
            .ToDictionaryAsync(x => x.Id, x => x.TitleFa + " " + x.TitleEn);
    }

    public Task<Dictionary<long, string>> GetBrandsByCategoryId(long categoryId)
    {
        return _brands
            .SelectMany(x => x.CategoryBrands)
            .Where(x => x.CategoryId == categoryId)
            .Include(x => x.Brand)
            .ToDictionaryAsync(x => x.BrandId,
                x => x.Brand.TitleFa + " " + x.Brand.TitleEn);
    }

    public Task<BrandDetailsViewModel> GetBrandDetails(long brandId)
    {
        return _mapper.ProjectTo<BrandDetailsViewModel>(
            _brands
        ).SingleOrDefaultAsync(x => x.Id == brandId);
    }

    public Task<Brand> GetInActiveBrand(long brandId)
    {
        return _brands.Where(x => !x.IsConfirmed)
            .SingleOrDefaultAsync(x => x.Id == brandId);
    }

    public Task<string> GetBrandTitle(long brandId)
    {
        return _brands.Where(x => x.Id == brandId)
            .Select(x => x.TitleFa)
            .SingleAsync();
    }

    public override async Task<DuplicateColumns> AddAsync(Brand entity)
    {
        var result = new List<string>();

        if (await _brands.AnyAsync(x => x.TitleFa == entity.TitleFa))
            result.Add(nameof(Brand.TitleFa));

        if (await _brands.AnyAsync(x => x.TitleEn == entity.TitleEn))
            result.Add(nameof(Brand.TitleEn));

        if (await _brands.AnyAsync(x => x.Slug == entity.Slug))
            result.Add(nameof(Brand.Slug));

        if (!result.Any())
            await base.AddAsync(entity);
        return new(!result.Any())
        {
            Columns = result
        };
    }

    public override async Task<DuplicateColumns> Update(Brand entity)
    {
        var query = _brands.Where(x => x.Id != entity.Id);
        var result = new List<string>();

        if (await query.AnyAsync(x => x.TitleFa == entity.TitleFa))
            result.Add(nameof(Brand.TitleFa));

        if (await query.AnyAsync(x => x.TitleEn == entity.TitleEn))
            result.Add(nameof(Brand.TitleEn));

        if (await query.AnyAsync(x => x.Slug == entity.Slug))
            result.Add(nameof(Brand.Slug));

        if (!result.Any())
            await base.Update(entity);
        return new(!result.Any())
        {
            Columns = result
        };
    }
}
