using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Orders;

namespace ProEShop.Services.Services;

public class ReturnProductItemService : GenericService<ReturnProductItem>, IReturnProductItemService
{
    private readonly DbSet<ReturnProductItem> _returnProductItems;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ReturnProductItemService(
        IUnitOfWork uow,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
        : base(uow)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _returnProductItems = uow.Set<ReturnProductItem>();
    }
	
    public Task<List<ReturnProductItemViewModel>> GetItemsByReturnProductId(long returnProductId)
    {
        var userId = _httpContextAccessor.HttpContext!.User.Identity.GetLoggedInUserId();
        return _mapper.ProjectTo<ReturnProductItemViewModel>(
                _returnProductItems.Where(x => x.ReturnProduct.Order.UserId == userId)
                    .Where(x => x.ReturnProductId == returnProductId)
                )
            .ToListAsync();
    }
}
