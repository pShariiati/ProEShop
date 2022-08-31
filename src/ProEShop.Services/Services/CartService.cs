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

public class CartService : CustomGenericService<Cart>, ICartService
{
    private readonly DbSet<Cart> _carts;

    public CartService(IUnitOfWork uow)
        : base(uow)
    {
        _carts = uow.Set<Cart>();
    }
}