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

    public async Task<int> GetVariantCodeForCreateProductVariant()
    {
        var lastProductVariantNumber = await _productVariants.OrderByDescending(x => x.Id)
            .Select(x => x.VariantCode)
            .FirstOrDefaultAsync();
        return lastProductVariantNumber + 1;
    }

    public async Task<ShowProductVariantInCreateConsignmentViewModel> GetProductVariantForCreateConsignment(int variantCode)
    {
        var sellerId = await _sellerService.GetSellerId();
        return await _mapper.ProjectTo<ShowProductVariantInCreateConsignmentViewModel>(
            _productVariants.Where(x => x.SellerId == sellerId)
        ).SingleOrDefaultAsync(x => x.VariantCode == variantCode);
    }

    public Task<List<GetProductVariantInCreateConsignmentViewModel>> GetProductVariantsForCreateConsignment(List<int> variantCodes)
    {
        var sellerId = _sellerService.GetSellerId().GetAwaiter().GetResult();
        //var sellerId = await _sellerService.GetSellerId();
        return _mapper.ProjectTo<GetProductVariantInCreateConsignmentViewModel>(
            _productVariants
                .Where(x => x.SellerId == sellerId)
                .Where(x => variantCodes.Contains(x.VariantCode))
        ).ToListAsync();
    }

    public Task<List<ProductVariant>> GetProductVariantsToAddCount(List<long> ids)
    {
        return _productVariants.Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<EditProductVariantViewModel> GetDataForEdit(long id)
    {
        var sellerId = await _sellerService.GetSellerId();
        return await _mapper.ProjectTo<EditProductVariantViewModel>(
            _productVariants.Where(x => x.SellerId == sellerId)
        ).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<AddEditDiscountViewModel> GetDataForAddEditDiscount(long id)
    {
        var sellerId = await _sellerService.GetSellerId();
        return await _mapper.ProjectTo<AddEditDiscountViewModel>(
            _productVariants.Where(x => x.SellerId == sellerId)
        ).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<ProductVariant> GetForEdit(long id)
    {
        var sellerId = await _sellerService.GetSellerId();
        return await _productVariants
            .Where(x => x.SellerId == sellerId)
            .SingleOrDefaultAsync(x => x.Id == id);
    }
}
