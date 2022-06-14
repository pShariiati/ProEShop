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
using ProEShop.ViewModels.ProductVariants;

namespace ProEShop.Services.Services;

public class ProductVariantService : GenericService<ProductVariant>, IProductVariantService
{
    private readonly DbSet<ProductVariant> _productVariants;
    private readonly IMapper _mapper;
    private readonly ISellerService _sellerService;

    public ProductVariantService(
        IUnitOfWork uow,
        IMapper mapper,
        ISellerService sellerService)
        : base(uow)
    {
        _mapper = mapper;
        _sellerService = sellerService;
        _productVariants = uow.Set<ProductVariant>();
    }

    public async Task<List<ShowProductVariantViewModel>> GetProductVariants(long productId)
    {
        var sellerId = await _sellerService.GetSellerId();
        return await _mapper.ProjectTo<ShowProductVariantViewModel>(
            _productVariants.Where(x => x.ProductId == productId)
                .Where(x => x.SellerId == sellerId)
        ).ToListAsync();
    }
}
