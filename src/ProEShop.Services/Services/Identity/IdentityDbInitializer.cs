using DNTCommon.Web.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProEShop.Common.GuardToolkit;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts;
using ProEShop.Services.Contracts.Identity;
using ProEShop.ViewModels.Identity.Settings;

namespace ProEShop.Services.Services.Identity;

public class IdentityDbInitializer : IIdentityDbInitializer
{
    private readonly IOptionsSnapshot<SiteSettings> _options;
    private readonly IApplicationUserManager _applicationUserManager;
    private readonly ILogger<IdentityDbInitializer> _logger;
    private readonly IApplicationRoleManager _roleManager;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IUnitOfWork _uow;
    private readonly IProvinceAndCityService _provinceAndCityService;
    private readonly IProductShortLinkService _productShortLinkService;

    public IdentityDbInitializer(
        IApplicationUserManager applicationUserManager,
        IServiceScopeFactory scopeFactory,
        IApplicationRoleManager roleManager,
        IOptionsSnapshot<SiteSettings> adminUserSeedOptions,
        ILogger<IdentityDbInitializer> logger, IUnitOfWork uow,
        IProvinceAndCityService provinceAndCityService,
        IProductShortLinkService productShortLinkService)
    {
        _applicationUserManager = applicationUserManager;
        _applicationUserManager.CheckArgumentIsNull(nameof(_applicationUserManager));

        _scopeFactory = scopeFactory;
        _scopeFactory.CheckArgumentIsNull(nameof(_scopeFactory));

        _roleManager = roleManager;
        _roleManager.CheckArgumentIsNull(nameof(_roleManager));

        _options = adminUserSeedOptions;
        _options.CheckArgumentIsNull(nameof(_options));

        _logger = logger;
        _uow = uow;
        _provinceAndCityService = provinceAndCityService;
        _productShortLinkService = productShortLinkService;
        _logger.CheckArgumentIsNull(nameof(_logger));
    }

    /// <summary>
    /// Applies any pending migrations for the context to the database.
    /// Will create the database if it does not already exist.
    /// </summary>
    public void Initialize()
    {
        _scopeFactory.RunScopedService<ApplicationDbContext>(context =>
        {
            context.Database.Migrate();
        });
    }


    /// <summary>
    /// Adds some default values to the IdentityDb
    /// </summary>
    public void SeedData()
    {
        _scopeFactory.RunScopedService<IIdentityDbInitializer>(identityDbSeedData =>
        {
            var result = identityDbSeedData.SeedAdminRole().Result;
            if (result == IdentityResult.Failed())
            {
                throw new InvalidOperationException(result.DumpErrors());
            }

            var sellerRole = identityDbSeedData.SeedSellerRole().Result;
            if (sellerRole == IdentityResult.Failed())
            {
                throw new InvalidOperationException(sellerRole.DumpErrors());
            }

            var warehouseRole = identityDbSeedData.SeedWarehouseRole().Result;
            if (warehouseRole == IdentityResult.Failed())
            {
                throw new InvalidOperationException(warehouseRole.DumpErrors());
            }

            identityDbSeedData.SeedProvincesAndCities().GetAwaiter().GetResult();
            identityDbSeedData.SeedProductShortLinks().GetAwaiter().GetResult();
        });
    }

    public async Task<IdentityResult> SeedAdminRole()
    {
        var thisMethodName = nameof(SeedAdminRole);

        //Create the `Admin` Role if it does not exist
        var adminRole = await _roleManager.FindByNameAsync(ConstantRoles.Admin);
        if (adminRole == null)
        {
            adminRole = new Role(ConstantRoles.Admin, "ادمین کل سیستم");
            var adminRoleResult = await _roleManager.CreateAsync(adminRole);
            if (adminRoleResult == IdentityResult.Failed())
            {
                _logger.LogError($"{thisMethodName}: adminRole CreateAsync failed. {adminRoleResult.DumpErrors()}");
                return IdentityResult.Failed();
            }

            await _uow.SaveChangesAsync();
        }
        else
        {
            _logger.LogInformation($"{thisMethodName}: adminRole already exists.");
        }

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> SeedSellerRole()
    {
        var thisMethodName = nameof(SeedAdminRole);
        //Create the `Seller` Role if it does not exist
        var sellerRole = await _roleManager.FindByNameAsync(ConstantRoles.Seller);
        if (sellerRole == null)
        {
            sellerRole = new Role(ConstantRoles.Seller, "فروشنده سیستم");
            var sellerRoleResult = await _roleManager.CreateAsync(sellerRole);
            if (sellerRoleResult == IdentityResult.Failed())
            {
                _logger.LogError($"{thisMethodName}: sellerRole CreateAsync failed. {sellerRoleResult.DumpErrors()}");
                return IdentityResult.Failed();
            }

            await _uow.SaveChangesAsync();
        }
        else
        {
            _logger.LogInformation($"{thisMethodName}: sellerRole already exists.");
        }

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> SeedWarehouseRole()
    {
        var thisMethodName = nameof(SeedWarehouseRole);
        //Create the `Warehouse` Role if it does not exist
        var warehouseRole = await _roleManager.FindByNameAsync(ConstantRoles.Warehouse);
        if (warehouseRole == null)
        {
            warehouseRole = new Role(ConstantRoles.Warehouse, "انباردار سیستم");
            var warehouseRoleResult = await _roleManager.CreateAsync(warehouseRole);
            if (warehouseRoleResult == IdentityResult.Failed())
            {
                _logger.LogError($"{thisMethodName}: warehouseRole CreateAsync failed. {warehouseRoleResult.DumpErrors()}");
                return IdentityResult.Failed();
            }

            await _uow.SaveChangesAsync();
        }
        else
        {
            _logger.LogInformation($"{thisMethodName}: warehouseRole already exists.");
        }

        return IdentityResult.Success;
    }

    public async Task SeedProvincesAndCities()
    {
        if (!await _provinceAndCityService.AnyAsync())
        {
            var p1 = new ProvinceAndCity()
            {
                Title = "تهران"
            };
            var c1 = new ProvinceAndCity()
            {
                Title = "تهران",
                Parent = p1
            };
            var c2 = new ProvinceAndCity()
            {
                Title = "شهریار",
                Parent = p1
            };
            var p2 = new ProvinceAndCity()
            {
                Title = "اصفهان"
            };
            var c3 = new ProvinceAndCity()
            {
                Title = "اصفهان",
                Parent = p2
            };
            var c4 = new ProvinceAndCity()
            {
                Title = "کاشان",
                Parent = p2
            };
            await _provinceAndCityService.AddAsync(p1);
            await _provinceAndCityService.AddAsync(p2);
            await _provinceAndCityService.AddAsync(c1);
            await _provinceAndCityService.AddAsync(c2);
            await _provinceAndCityService.AddAsync(c3);
            await _provinceAndCityService.AddAsync(c4);
            await _uow.SaveChangesAsync();
        }
    }

    public async Task SeedProductShortLinks()
    {
        if (!await _productShortLinkService.AnyAsync())
        {
            var links = new List<Entities.ProductShortLink>();

            for (var letter1 = 'A'; letter1 <= 'z'; letter1++)
            {
                if (!char.IsLetterOrDigit(letter1))
                    continue;
                var link = $"{letter1.ToString()}";
                links.Add(new ProductShortLink()
                {
                    Link = link
                });
                //for (var letter2 = '0'; letter2 <= 'z'; letter2++)
                //{
                //    if (!char.IsLetterOrDigit(letter2))
                //        continue;
                //    //var link = $"{letter1.ToString()}{letter2}";
                //    //links.Add(new ProductShortLink()
                //    //{
                //    //    Link = link
                //    //});
                //    for (var letter3 = '0'; letter3 <= 'z'; letter3++)
                //    {
                //        if (!char.IsLetterOrDigit(letter3))
                //            continue;
                //        var link = $"{letter1.ToString()}{letter2}{letter3}";
                //        links.Add(new ProductShortLink()
                //        {
                //            Link = link
                //        });
                //    }
                //}
            }

            await _productShortLinkService.AddRangeAsync(links);
            await _uow.SaveChangesAsync();
        }
    }
}
