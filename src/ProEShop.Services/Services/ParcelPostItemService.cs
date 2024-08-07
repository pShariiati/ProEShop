﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Entities.Enums;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Services;

public class ParcelPostItemService : CustomGenericService<ParcelPostItem>, IParcelPostItemService
{
    private readonly DbSet<ParcelPostItem> _parcelPostItems;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ParcelPostItemService(
        IUnitOfWork uow,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(uow)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _parcelPostItems = uow.Set<ParcelPostItem>();
    }

    public async Task<ShowProductsInProfileCommentViewModel> GetProductsInProfileComment(ShowProductsInProfileCommentViewModel model)
    {
        var userId = _httpContextAccessor.HttpContext.User.Identity.GetLoggedInUserId();

        var parcelPostItems = _parcelPostItems.AsNoTracking()
            .Where(x => x.ParcelPost.Order.UserId == userId)
            .Where(x => x.ParcelPost.Order.Status == OrderStatus.DeliveredToClient)
            .Where(x => x.ProductVariant.Product.ProductComments.Any(pc => pc.UserId == userId) == false)
            .GroupBy(x => x.ProductVariant.ProductId)
            .OrderByDescending(x => x.First().CreatedDateTime);

        var paginationResult = await GenericPagination2Async(parcelPostItems, model.Pagination);

        return new()
        {
            Items = await _mapper.ProjectTo<ShowProductInProfileCommentViewModel>(
                paginationResult.Query
            ).ToListAsync(),
            Pagination = paginationResult.Pagination
        };
    }

    public Task<ShowProductsInProfileCommentViewModel> GetProductsInProfileComment(int pageNumber)
    {
        var model = new ShowProductsInProfileCommentViewModel();
        model.Pagination.CurrentPage = pageNumber;
        return GetProductsInProfileComment(model);
    }

    public Task<bool> CheckBrandIdForExistingInOrder(long orderId, long brandId)
    {
        return _parcelPostItems.Where(x => x.OrderId == orderId)
            .AnyAsync(x => x.ProductVariant.Product.BrandId == brandId);
    }

    public Task<bool> CheckCategoryIdForExistingInOrder(long orderId, long categoryId)
    {
        return _parcelPostItems.Where(x => x.OrderId == orderId)
            .SelectMany(x => x.ProductVariant.Product.ProductCategories)
            .AnyAsync(x => x.CategoryId == categoryId);
    }

    public async Task<bool> CheckProductsVariantsForReturn(long orderId, List<long> productVariantIdsToReturn, long userId)
    {
        var returnProductsCount = await _parcelPostItems
            .Where(x => productVariantIdsToReturn.Contains(x.ProductVariantId))
            .Where(x => x.OrderId == orderId)
            .CountAsync(x => x.Order.UserId == userId);

        return returnProductsCount == productVariantIdsToReturn.Count;
    }
}