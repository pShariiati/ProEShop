using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
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

    public async Task<(bool IsSuccessful, bool IsVariantNull)> CheckProductAndVariantTypeForForAddVariant(long productId, long variantId)
    {
        var product = await _products
            .Select(x => new
            {
                x.Id,
                x.Category.IsVariantColor
            }).SingleOrDefaultAsync(x => x.Id == productId);
        if (product is null)
            return (false, default);

        if (product.IsVariantColor is null)
            return (true, true);

        var variant = await _variants
            .Where(x => x.IsConfirmed)
            .Select(x => new
            {
                x.Id,
                x.IsColor
            }).SingleOrDefaultAsync(x => x.Id == variantId);
        if (variant is null)
            return (false, false);

        return (product.IsVariantColor == variant.IsColor, false);
    }

    public Task<List<ShowVariantInEditCategoryVariantViewModel>> GetVariantsForEditCategoryVariants(bool isColor)
    {
        return _mapper.ProjectTo<ShowVariantInEditCategoryVariantViewModel>(
                _variants.Where(x => x.IsConfirmed)
                    .Where(x => x.IsColor == isColor)
        ).ToListAsync();
    }

    public async Task<bool> CheckVariantsCountAndConfirmStatusForEditCategoryVariants(List<long> variantsIds, bool isColor)
    {
        var result = await _variants
            .Where(x => variantsIds.Contains(x.Id))
            .Where(x => x.IsColor == isColor)
            .CountAsync(x => x.IsConfirmed);
        return variantsIds.Count == result;
    }
}
