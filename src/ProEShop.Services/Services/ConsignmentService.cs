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

public class ConsignmentService : GenericService<Consignment>, IConsignmentService
{
    private readonly DbSet<Consignment> _consignments;

    public ConsignmentService(IUnitOfWork uow)
        : base(uow)
    {
        _consignments = uow.Set<Consignment>();
    }

    
}
