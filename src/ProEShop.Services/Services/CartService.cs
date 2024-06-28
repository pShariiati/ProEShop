using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.Services.Contracts.Identity;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Services;

public class CartService : CustomGenericService<Cart>, ICartService
{
    private readonly DbSet<Cart> _carts;
    private readonly IMapper _mapper;
    private readonly IApplicationUserManager _userManager;

    public CartService(
        IUnitOfWork uow,
        IMapper mapper,
        IApplicationUserManager userManager)
        : base(uow)
    {
        _mapper = mapper;
        _userManager = userManager;
        _carts = uow.Set<Cart>();
    }

    public Task<List<ProductVariantInCartForProductInfoViewModel>> GetProductVariantsInCart(List<long> productVariantsIds, long userId)
    {
        return _mapper.ProjectTo<ProductVariantInCartForProductInfoViewModel>(
            _carts
                .Where(x => x.UserId == userId)
                .Where(x => productVariantsIds.Contains(x.ProductVariantId))
        ).ToListAsync();
    }

    public Task<List<ShowCartInDropDownViewModel>> GetCartsForDropDown(long userId)
    {
        return _carts.AsNoTracking()
            .Where(x => x.UserId == userId)
            .ProjectTo<ShowCartInDropDownViewModel>(
                configuration: _mapper.ConfigurationProvider, parameters: new { now = DateTime.Now }
            ).ToListAsync();
    }

    public Task<List<ShowCartInCartPageViewModel>> GetCartsForCartPage(long userId)
    {
        return _carts.AsNoTracking()
            .Where(x => x.UserId == userId)
            .ProjectTo<ShowCartInCartPageViewModel>(
                configuration: _mapper.ConfigurationProvider, parameters: new { now = DateTime.Now }
            ).ToListAsync();
    }

    public Task<List<ShowCartInCheckoutPageViewModel>> GetCartsForCheckoutPage(long userId)
    {
        return _carts.AsNoTracking()
            .Where(x => x.UserId == userId)
            .ProjectTo<ShowCartInCheckoutPageViewModel>(
                configuration: _mapper.ConfigurationProvider, parameters: new { now = DateTime.Now }
            ).ToListAsync();
    }

    public Task<List<ShowCartInPaymentPageViewModel>> GetCartsForPaymentPage(long userId)
    {
        return _carts.AsNoTracking()
            .Where(x => x.UserId == userId)
            .ProjectTo<ShowCartInPaymentPageViewModel>(
                configuration: _mapper.ConfigurationProvider, parameters: new { now = DateTime.Now }
            ).ToListAsync();
    }

    public Task<List<ShowCartForCreateOrderAndPayViewModel>> GetCartsForCreateOrderAndPay(long userId)
    {
        return _carts.AsNoTracking()
            .Where(x => x.UserId == userId)
            .ProjectTo<ShowCartForCreateOrderAndPayViewModel>(
                configuration: _mapper.ConfigurationProvider, parameters: new { now = DateTime.Now }
            ).ToListAsync();
    }

    public Task<List<Cart>> GetAllCartItems(long userId)
    {
        return _carts.Where(x => x.UserId == userId).ToListAsync();
    }

    public Task<bool> CheckBrandIdForExistingInCart(long brandId)
    {
        var userId = _userManager.GetLoggedInUser();

        return _carts.Where(x => x.UserId == userId)
            .AnyAsync(x => x.ProductVariant.Product.BrandId == brandId);
    }

    public Task<bool> CheckCategoryIdForExistingInCart(long categoryId)
    {
        var userId = _userManager.GetLoggedInUser();

        return _carts.Where(x => x.UserId == userId)
            .SelectMany(x => x.ProductVariant.Product.ProductCategories)
            .AnyAsync(x => x.CategoryId == categoryId);
    }
}