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

public class AddressService : GenericService<Address>, IAddressService
{
    private readonly DbSet<Address> _addresses;
    private readonly IMapper _mapper;

    public AddressService(IUnitOfWork uow, IMapper mapper)
        : base(uow)
    {
        _mapper = mapper;
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
}
