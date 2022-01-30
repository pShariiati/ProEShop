using AutoMapper;
using DNTPersianUtils.Core;
using MD.PersianDateTime.Standard;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProEShop.DataLayer.Context;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts.Identity;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Services.Services.Identity;

public class ApplicationUserManager
    : UserManager<User>, IApplicationUserManager
{
    private readonly DbSet<User> _users;
    private readonly IMapper _mapper;

    public ApplicationUserManager(
        IApplicationUserStore store,
        IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<User> passwordHasher,
        IEnumerable<IUserValidator<User>> userValidators,
        IEnumerable<IPasswordValidator<User>> passwordValidators,
        ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors,
        IServiceProvider services,
        ILogger<ApplicationUserManager> logger,
        IUnitOfWork uow, IMapper mapper)
        : base(
            (UserStore<User, Role, ApplicationDbContext, long, UserClaim, UserRole, UserLogin, UserToken,
                RoleClaim>)store,
            optionsAccessor, passwordHasher, userValidators, passwordValidators,
            keyNormalizer, errors, services, logger)
    {
        _mapper = mapper;
        _users = uow.Set<User>();
    }

    #region CustomClass

    public async Task<DateTime?> GetSendSmsLastTimeAsync(string phoneNumber)
    {
        var result = await _users.Select(x => new
        {
            x.UserName,
            x.SendSmsLastTime
        })
                    .SingleOrDefaultAsync(x => x.UserName == phoneNumber);
        return result?.SendSmsLastTime;
    }

    public async Task<bool> CheckForUserIsSeller(string phoneNumber)
    {
        return await _users.Where(x => x.UserName == phoneNumber)
            .Where(x => x.UserRoles.All(r => r.Role.Name != ConstantRoles.Seller))
            .AnyAsync(x => x.IsSeller);
    }

    public async Task<CreateSellerViewModel> GetUserInfoForCreateSeller(string phoneNumber)
    {
        var result = await _mapper.ProjectTo<CreateSellerViewModel>(
                _users)
            .SingleOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        if (result?.BirthDate != null)
        {
            var parsedDateTime = DateTime.Parse(result.BirthDate);
            var persianDateTime = new PersianDateTime(parsedDateTime)
            {
                PersianNumber = true
            };
            result.BirthDateEn = parsedDateTime.ToString("yyyy/MM/dd");
            result.BirthDate = persianDateTime.ToShortDateString();
        }
        return result;
    }

    #endregion
}
