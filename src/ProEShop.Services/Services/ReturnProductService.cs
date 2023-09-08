using Microsoft.EntityFrameworkCore;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;

namespace ProEShop.Services.Services;

public class ReturnProductService : GenericService<ReturnProduct>, IReturnProductService
{
    private readonly DbSet<ReturnProduct> _returnProducts;

    public ReturnProductService(
        IUnitOfWork uow)
        : base(uow)
    {
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
}
