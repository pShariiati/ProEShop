using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Categories;

namespace ProEShop.Services.Services;

public class ProvinceAndCityService : GenericService<ProvinceAndCity>, IProvinceAndCityService
{
    private readonly DbSet<ProvinceAndCity> _provinceAndCities;
    public ProvinceAndCityService(IUnitOfWork uow)
        : base(uow)
    {
        _provinceAndCities = uow.Set<ProvinceAndCity>();
    }

    public Task<Dictionary<long, string>> GetProvincesToShowInSelectBoxAsync()
    {
        return _provinceAndCities.Where(x => x.ParentId == null)
            .ToDictionaryAsync(x => x.Id, x => x.Title);
    }

    public async Task<Dictionary<long, string>> GetCitiesByProvinceIdInSelectBoxAsync(long provinceId)
    {
        return await _provinceAndCities.Where(x => x.ParentId == provinceId)
            .ToDictionaryAsync(x => x.Id, x => x.Title);
    }

    public async Task<(long, long)> GetForSeedData()
    {
        var province = await _provinceAndCities.Where(x => x.ParentId == null)
            .Select(x => new
            {
                x.Title,
                x.Id
            })
            .SingleAsync(x => x.Title == "اصفهان");

        var city = await _provinceAndCities.Where(x => x.ParentId != null)
            .Select(x => new
            {
                x.Title,
                x.Id
            })
            .SingleAsync(x => x.Title == "کاشان");
        return (province.Id, city.Id);
    }
}
