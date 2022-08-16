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
using ProEShop.ViewModels.Variants;

namespace ProEShop.Services.Services;

public class VariantService : GenericService<Variant>, IVariantService
{
    private readonly DbSet<Variant> _variants;
    private readonly DbSet<Product> _products;
    private readonly IMapper _mapper;

    public VariantService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _variants = uow.Set<Variant>();
        _products = uow.Set<Product>();
    }

    public async Task<ShowVariantsViewModel> GetVariants(ShowVariantsViewModel model)
    {
        var variants = _variants.AsNoTracking().AsQueryable();

        #region Search

        variants = ExpressionHelpers.CreateSearchExpressions(variants, model.SearchVariants, callDeletedStatusExpression: false);

        #endregion

        #region OrderBy

        variants = variants.CreateOrderByExpression(model.SearchVariants.Sorting.ToString(),
            model.SearchVariants.SortingOrder.ToString());

        #endregion

        var paginationResult = await GenericPaginationAsync(variants, model.Pagination);

        return new()
        {
            Variants = await _mapper.ProjectTo<ShowVariantViewModel>(
                    paginationResult.Query
                ).ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }

    public async Task<bool> CheckProductAndVariantTypeForForAddVariant(long productId, long variantId)
    {
        var product = await _products
            .Select(x => new
            {
                x.Id,
                x.Category.IsVariantColor
            }).SingleOrDefaultAsync(x => x.Id == productId);
        if (product is null)
            return false;

        var variant = await _variants
            .Where(x => x.IsConfirmed)
            .Select(x => new
            {
                x.Id,
                x.IsColor
            }).SingleOrDefaultAsync(x => x.Id == variantId);
        if (variant is null)
            return false;

        return product.IsVariantColor == variant.IsColor;
    }

    public Task<List<ShowVariantInEditCategoryVariantViewModel>> GetVariantsForEditCategoryVariants(bool isColor)
    {
        return _mapper.ProjectTo<ShowVariantInEditCategoryVariantViewModel>(
                _variants.Where(x => x.IsConfirmed)
                    .Where(x => x.IsColor == isColor)
        ).ToListAsync();
    }
}
