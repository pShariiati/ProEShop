using ProEShop.Entities;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.Sellers;

namespace ProEShop.Services.Contracts;

public interface ISellerService : IGenericService<Seller>
{
    Task<int> GetSellerCodeForCreateSeller();
    Task<ShowSellersViewModel> GetSellers(ShowSellersViewModel model);
    Task<SellerDetailsViewModel> GetSellerDetails(long id);
    Task<Seller> GetSellerToRemoveInManagingSellers(long id);
    Task<long> GetSellerId(long userId);
    Task<long> GetSellerId();

    /// <summary>
    /// اگر کاربرِ داخلِ سیستم، فروشنده باشه
    /// آیدی فروشنده برگشت داده میشه
    /// </summary>
    /// <returns></returns>
    Task<long?> GetSellerId2();

    Task<List<string>> GetShopNamesForAutocomplete(string input);
}