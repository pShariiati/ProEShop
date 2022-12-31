using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Categories;

namespace ProEShop.Services.Services;

public class ProductCommentService : GenericService<ProductComment>, IProductCommentService
{
    private readonly DbSet<ProductComment> _productComments;
    private readonly IMapper _mapper;

    public ProductCommentService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _productComments = uow.Set<ProductComment>();
    }
}
