using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.IdentityToolkit;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.UserHistories;

namespace ProEShop.Web.Pages.Profile.UserHistory;

public class IndexModel : ProfilePageBase
{
    #region Constructor

    private readonly IUserHistoryService _userHistoryService;
    private readonly IUnitOfWork _uow;

    public IndexModel(
            IUserHistoryService userHistoryService,
            IUnitOfWork uow)
    {
        _userHistoryService = userHistoryService;
        _uow = uow;
    }

    #endregion

    public List<ShowUserHistoryViewModel> Products { get; set; }

    public async Task OnGet()
    {
        Products = await _userHistoryService.GetUserHistories();
    }

    public async Task<IActionResult> OnPost(long productId)
    {
        var userId = User.Identity.GetLoggedInUserId();

        var userHistory = await _userHistoryService.FindAsync(userId, productId);

        if (userHistory != null)
        {
            _userHistoryService.Remove(userHistory);
            await _uow.SaveChangesAsync();
        }

        return JsonOk("محصول مورد نظر با موفقیت از بازدید های اخیر شما حذف شد",new
        {
            ProductId = productId
        });
    }
}