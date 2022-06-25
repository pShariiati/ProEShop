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
using ProEShop.ViewModels.ConsignmentItems;
using ProEShop.ViewModels.Consignments;

namespace ProEShop.Services.Services;

public class ConsignmentItemService : GenericService<ConsignmentItem>, IConsignmentItemService
{
    private readonly DbSet<ConsignmentItem> _consignmentItems;
    private readonly IMapper _mapper;

    public ConsignmentItemService(
        IUnitOfWork uow,
        IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
        _consignmentItems = uow.Set<ConsignmentItem>();
    }

    public Task<List<ShowConsignmentItemViewModel>> GetConsignmentItems(long consignmentId)
    {
        return _mapper.ProjectTo<ShowConsignmentItemViewModel>(
            _consignmentItems
        ).ToListAsync();
    }
}
