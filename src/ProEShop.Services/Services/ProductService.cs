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

public class ProductService : GenericService<Product>, IProductService
{
    private readonly DbSet<Product> _products;

    public ProductService(IUnitOfWork uow)
        : base(uow)
    {
        _products = uow.Set<Product>();
    }
    
}
