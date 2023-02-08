using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.Helpers;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Entities.Enums;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.ProductComments;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Services;

public class ProductCommentService : GenericService<ProductComment>, IProductCommentService
{
    private readonly DbSet<ProductComment> _productComments;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISellerService _sellerService;

    public ProductCommentService(
        IUnitOfWork uow,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        ISellerService sellerService)
        : base(uow)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _sellerService = sellerService;
        _productComments = uow.Set<ProductComment>();
    }

    public async Task<List<ProductCommentForProductInfoViewModel>> GetCommentsByPagination(long productId, int pageNumber, CommentsSortingForProductInfo sortBy, SortingOrder orderBy)
    {
        var query = _productComments
            .Where(x => x.IsConfirmed.Value)
            .Where(x => x.ProductId == productId);

        #region OrderBy

        if (sortBy == CommentsSortingForProductInfo.MostUseful)
        {
            // بر اساس تفریق دیسلایک از لایک
            // اگر یک کامنت 15 لایک داشته باشد و 14 دیسلایک، حاصل میشه مثبت یک
            // ولی اگر یک کامنت فقط دو لایک بدون دیسلایک داشته باشد حاصل میشه مثبت دو
            // پس کامنتی که فقط دو لایک خالی دارد بالاتر از کامنتی که 15 لایک دارد نمایش داده میشود

            query = query.OrderByDescending(x => x.CommentsScores
                .LongCount(c => c.IsLike) - x.CommentsScores.LongCount(c => !c.IsLike));
        }
        else
        {
            query = query.CreateOrderByExpression(sortBy.ToString(), orderBy.ToString());
        }

        #endregion

        query = await GenericPaginationAsync(query, pageNumber, 2);

        return await _mapper.ProjectTo<ProductCommentForProductInfoViewModel>(query)
            .ToListAsync();
    }

    public async Task<ShowProductCommentsInProfile> GetCommentsInProfileComment(ShowProductCommentsInProfile model)
    {
        var userId = _httpContextAccessor.HttpContext.User.Identity.GetLoggedInUserId();
        var sellerId = await _sellerService.GetSellerId2() ?? 0;

        var parcelPostItems = _productComments.AsNoTracking()
            .Where(x => x.UserId == userId || x.SellerId == sellerId)
            .OrderByDescending(x => x.Id);

        var paginationResult = await GenericPaginationAsync(parcelPostItems, model.Pagination);

        return new()
        {
            Items = await _mapper.ProjectTo<ShowProductCommentInProfile>(
                paginationResult.Query
            ).ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }
}
