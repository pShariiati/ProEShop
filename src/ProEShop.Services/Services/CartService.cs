using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Services;

public class CartService : CustomGenericService<Cart>, ICartService
{
    private readonly DbSet<Cart> _carts;
    private readonly IMapper _mapper;

    public CartService(
        IUnitOfWork uow,
        IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
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
}