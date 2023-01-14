﻿using ProEShop.Entities;
using ProEShop.ViewModels;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Carts;
using ProEShop.ViewModels.ProductComments;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Contracts;

public interface IProductCommentService : IGenericService<ProductComment>
{
    /// <summary>
    /// گرفتن نظرات محصولات به صورت صفحه بندی شده
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="pageNumber"></param>
    /// <param name="sortBy"></param>
    /// <param name="orderBy"></param>
    /// <returns></returns>
    Task<List<ProductCommentForProductInfoViewModel>> GetCommentsByPagination(long productId, int pageNumber, CommentsSortingForProductInfo sortBy, SortingOrder orderBy);
}