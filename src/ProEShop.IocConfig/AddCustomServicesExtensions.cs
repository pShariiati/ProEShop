﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProEShop.DataLayer.Context;
using ProEShop.Entities.Identity;
using ProEShop.Services.Contracts;
using ProEShop.Services.Contracts.Identity;
using ProEShop.Services.Services;
using ProEShop.Services.Services.Identity;
using System.Security.Claims;
using System.Security.Principal;
using Ganss.Xss;

namespace ProEShop.IocConfig;

public static class AddCustomServicesExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IViewRendererService, ViewRendererService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IPrincipal>(provider =>
            provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.User ?? ClaimsPrincipal.Current);

        services.AddScoped<IUserClaimsPrincipalFactory<User>, ApplicationClaimsPrincipalFactory>();
        services.AddScoped<UserClaimsPrincipalFactory<User, Role>, ApplicationClaimsPrincipalFactory>();

        services.AddScoped<IdentityErrorDescriber, CustomIdentityErrorDescriber>();

        services.AddScoped<IApplicationUserStore, ApplicationUserStore>();
        services
            .AddScoped<UserStore<User, Role, ApplicationDbContext, long, UserClaim, UserRole, UserLogin, UserToken,
                RoleClaim>, ApplicationUserStore>();

        services.AddScoped<IApplicationRoleStore, ApplicationRoleStore>();
        services
            .AddScoped<RoleStore<Role, ApplicationDbContext, long, UserRole, RoleClaim>, ApplicationRoleStore>();

        services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
        services.AddScoped<UserManager<User>, ApplicationUserManager>();

        services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();
        services.AddScoped<RoleManager<Role>, ApplicationRoleManager>();

        services.AddScoped<IApplicationSignInManager, ApplicationSignInManager>();
        services.AddScoped<SignInManager<User>, ApplicationSignInManager>();

        services.AddScoped<IIdentityDbInitializer, IdentityDbInitializer>();

        services.AddScoped<ISmsSender, AuthMessageSender>();
        services.AddScoped<IHttpClientService, HttpClientService>();


        services.AddScoped<IUploadFileService, UploadFileService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IFeatureService, FeatureService>();
        services.AddScoped<ICategoryFeatureService, CategoryFeatureService>();
        services.AddScoped<IProvinceAndCityService, ProvinceAndCityService>();
        services.AddScoped<ISellerService, SellerService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IFeatureConstantValueService, FeatureConstantValueService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryBrandService, CategoryBrandService>();
        services.AddScoped<IVariantService, VariantService>();
        services.AddScoped<IGuaranteeService, GuaranteeService>();
        services.AddScoped<IProductVariantService, ProductVariantService>();
        services.AddScoped<IConsignmentService, ConsignmentService>();
        services.AddScoped<IConsignmentItemService, ConsignmentItemService>();
        services.AddScoped<IProductStockService, ProductStockService>();
        services.AddScoped<IUserProductFavoriteService, UserProductFavoriteService>();
        services.AddScoped<IProductShortLinkService, ProductShortLinkService>();
        services.AddScoped<ICategoryVariantService, CategoryVariantService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IParcelPostService, ParcelPostService>();
        services.AddScoped<ICommentReportService, CommentReportService>();
        services.AddScoped<IProductCommentService, ProductCommentService>();
        services.AddScoped<ICommentScoreService, CommentScoreService>();
        services.AddScoped<IAnswerScoreService, AnswerScoreService>();
        services.AddScoped<IQuestionAndAnswerService, QuestionAndAnswerService>();
        services.AddScoped<IDiscountNoticeService, DiscountNoticeService>();
        services.AddScoped<IUserListShortLinkService, UserListShortLinkService>();
        services.AddScoped<IUserListService, UserListService>();
        services.AddScoped<IUserListProductService, UserListProductService>();
        services.AddScoped<IParcelPostItemService, ParcelPostItemService>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<IUserHistoryService, UserHistoryService>();
        services.AddScoped<IDiscountCodeService, DiscountCodeService>();
        services.AddScoped<IDiscountCodeService, DiscountCodeService>();
        services.AddScoped<IUsedDiscountCodeService, UsedDiscountCodeService>();
        services.AddScoped<IGiftCardService, GiftCardService>();
        services.AddScoped<IReturnProductService, ReturnProductService>();
        services.AddScoped<IReturnProductItemService, ReturnProductItemService>();

        #region Html sanitizer
        IHtmlSanitizer sanitizer = new HtmlSanitizer();
        //services.AddSingleton<IHtmlSanitizer, HtmlSanitizer>();
        services.AddSingleton(sanitizer);
        #endregion

        return services;
    }
}
