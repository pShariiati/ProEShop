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

public class ProductStockService : GenericService<ProductStock>, IProductStockService
{
    private readonly DbSet<ProductStock> _productStocks;
    private readonly IMapper _mapper;

    public ProductStockService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _productStocks = uow.Set<ProductStock>();
    }

    public Task<ProductStock> GetByProductVariantIdAndConsignmentId(long productVariantId, long consignmentId)
    {
        return _productStocks.Where(x => x.ConsignmentId == consignmentId)
            .SingleOrDefaultAsync(x => x.ProductVariantId == productVariantId);
    }

    public Task<Dictionary<long, int>> GetProductStocksForAddProductVariantsCount(long consignmentId)
    {
        return _productStocks.Where(x => x.ConsignmentId == consignmentId)
            .ToDictionaryAsync(x => x.ProductVariantId, x => x.Count);
    }
}
