﻿using ProEShop.Entities;
using ProEShop.ViewModels.Categories;

namespace ProEShop.Services.Contracts;

public interface ICategoryService : IGenericService<Category>
{
    Task<ShowCategoriesViewModel> GetCategories(ShowCategoriesViewModel model);
    Task<Dictionary<long, string>> GetCategoriesToShowInSelectBoxAsync(long? id = null);
    Task<EditCategoryViewModel> GetForEdit(long id);
    Task<List<List<ShowCategoryForCreateProductViewModel>>> GetCategoriesForCreateProduct(long[] selectedCategoriesIds);
    Task<List<string>> GetCategoryBrands(long categoryId);
    Task<Category> GetCategoryWithItsBrands(long categoryId);
    Task<bool> CanAddFakeProduct(long categoryId);
    Task<(bool isSuccessful, List<long> categoryIds)> GetCategoryParentIds(long categoryId);
}