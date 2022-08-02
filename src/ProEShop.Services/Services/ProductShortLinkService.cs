using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.ProductShortLinks;

namespace ProEShop.Services.Services;

public class ProductShortLinkService : GenericService<ProductShortLink>, IProductShortLinkService
{
    private readonly DbSet<ProductShortLink> _productShortLinks;
    private readonly IMapper _mapper;

    public ProductShortLinkService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _productShortLinks = uow.Set<ProductShortLink>();
    }

    public async Task<ShowProductShortLinksViewModel> GetProductShortLinks(ShowProductShortLinksViewModel model)
    {
        var productShortLinks = _productShortLinks.AsNoTracking().AsQueryable();

        #region Search

        productShortLinks = ExpressionHelpers
            .CreateSearchExpressions(productShortLinks, model.SearchBrands, callDeletedStatusExpression: false);

        #endregion

        #region OrderBy

        productShortLinks = productShortLinks.CreateOrderByExpression(model.SearchBrands.Sorting.ToString(),
            model.SearchBrands.SortingOrder.ToString());

        #endregion

        var paginationResult = await GenericPaginationAsync(productShortLinks, model.Pagination);

        return new()
        {
            ProductShortLinks = await _mapper.ProjectTo<ShowProductShortLinkViewModel>(
                paginationResult.Query
            ).ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }

    public Task<ProductShortLink> GetProductShortLinkForCreateProduct()
    {
        return _productShortLinks
            .OrderBy(x => Guid.NewGuid())
            .FirstAsync();
    }

    public Task<ProductShortLink> GetForDelete(long id)
    {
        return _productShortLinks
            .Where(x => !x.IsUsed)
            .SingleOrDefaultAsync(x => x.Id == id);
    }
}
