using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Categories;

namespace ProEShop.Services.Services;

public class SellerService : GenericService<Seller>, ISellerService
{
    public SellerService(IUnitOfWork uow) : base(uow)
    {
    }
}
