using ProEShop.Entities;
using ProEShop.ViewModels.CategoryFeatures;
using ProEShop.ViewModels.Products;

namespace ProEShop.Services.Contracts;

public interface IParcelPostItemService : ICustomGenericService<ParcelPostItem>
{
    /// <summary>
    /// محصولات بخش نظرات در بخش پروفایل کاربری
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ShowProductsInProfileCommentViewModel> GetProductsInProfileComment(ShowProductsInProfileCommentViewModel model);

    /// <summary>
    /// این متد از متد بالایی استفاده میکند، اینجا فقط بهش شماره صحفه پاس میدیم
    /// </summary>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    Task<ShowProductsInProfileCommentViewModel> GetProductsInProfileComment(int pageNumber);
}