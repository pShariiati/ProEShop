using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.UserHistories;

namespace ProEShop.Services.Services;

public class UserHistoryService : CustomGenericService<UserHistory>, IUserHistoryService
{
    private readonly DbSet<UserHistory> _userHistories;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserHistoryService(
        IUnitOfWork uow,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(uow)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _userHistories = uow.Set<UserHistory>();
    }

    public Task<List<ShowUserHistoryViewModel>> GetUserHistories()
    {
        var userId = _httpContextAccessor.HttpContext.User.Identity.GetLoggedInUserId();

        return _userHistories.Where(x => x.UserId == userId)
            .AsNoTracking()
            .Where(x => x.Product.ProductStockStatus == ProductStockStatus.Available)
            .OrderBy(x => x.CreatedDateTime)
            .Take(10).ProjectTo<ShowUserHistoryViewModel>(
                configuration: _mapper.ConfigurationProvider,
                parameters: new { userId }
            )
            .ToListAsync();
    }
}