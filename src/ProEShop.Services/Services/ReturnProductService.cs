using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;

namespace ProEShop.Services.Services;

public class ReturnProductService : GenericService<ReturnProduct>, IReturnProductService
{
    private readonly DbSet<ReturnProduct> _returnProducts;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ReturnProductService(
        IUnitOfWork uow,
        IHttpContextAccessor httpContextAccessor)
        : base(uow)
    {
        _httpContextAccessor = httpContextAccessor;
        _returnProducts = uow.Set<ReturnProduct>();
    }

    public async Task<long> GetTrackingNumberForAddNewRecord()
    {
        var lastTackingNumber = await _returnProducts
            .OrderByDescending(x => x.Id)
            .Select(x => x.TrackingNumber)
            .FirstOrDefaultAsync();
        return lastTackingNumber + 1;
    }

    public Task<bool> CheckForReturnProductDetails(long id)
    {
        var userId = _httpContextAccessor.HttpContext.User.Identity.GetLoggedInUserId();
        return _returnProducts.Where(x => x.Id == id)
            .AnyAsync(x => x.Order.UserId == userId);
    }
}
