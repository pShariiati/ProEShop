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

public class ProductVariantService : GenericService<ProductVariant>, IProductVariantService
{
    private readonly DbSet<ProductVariant> _productVariants;

    public ProductVariantService(IUnitOfWork uow)
        : base(uow)
    {
        _productVariants = uow.Set<ProductVariant>();
    }
}
