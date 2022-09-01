﻿using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
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
}