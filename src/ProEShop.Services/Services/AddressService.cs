using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Addresses;
using ProEShop.ViewModels.Carts;

namespace ProEShop.Services.Services;

public class AddressService : GenericService<Address>, IAddressService
{
    private readonly DbSet<Address> _addresses;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AddressService(
        IUnitOfWork uow,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
        : base(uow)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _addresses = uow.Set<Address>();
    }

    public Task<AddressInCheckoutPageViewModel> GetAddressForCheckoutPage(long userId)
    {
        return _mapper.ProjectTo<AddressInCheckoutPageViewModel>(
                _addresses.Where(x => x.UserId == userId)
            ).FirstAsync();
    }

    public async Task<(bool HasUserAddress, long AddressId)> GetAddressForCreateOrderAndPay(long userId)
    {
        var address = await _addresses
            .Where(x => x.IsDefault)
            .Select(x => new
            {
                x.Id,
                x.UserId
            }).FirstOrDefaultAsync(x => x.UserId == userId);
        if (address is null)
            return (false, default);
        return (true, address.Id);
    }

    public Task<List<ShowAddressInProfileViewModel>> GetAllUserAddresses()
    {
        var userId = _httpContextAccessor.HttpContext.User.Identity.GetLoggedInUserId();

        return _mapper.ProjectTo<ShowAddressInProfileViewModel>(
                _addresses.Where(x => x.UserId == userId)
            ).ToListAsync();
    }

    public async Task<bool> RemoveUserAddress(long id)
    {
        var userId = _httpContextAccessor.HttpContext.User.Identity.GetLoggedInUserId();

        var addressToRemove = await _addresses.Where(x => x.UserId == userId)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (addressToRemove is null)
            return false;

        _addresses.Remove(addressToRemove);

        return true;
    }
}
