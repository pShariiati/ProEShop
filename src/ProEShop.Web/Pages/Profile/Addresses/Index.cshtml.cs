using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Constants;
using ProEShop.DataLayer.Context;
using ProEShop.Services.Contracts;
using ProEShop.ViewModels.Addresses;

namespace ProEShop.Web.Pages.Profile.Addresses;

public class IndexModel : ProfilePageBase
{
    #region Constructor

    private readonly IAddressService _addressService;
    private readonly IUnitOfWork _uow;

    public IndexModel(
        IAddressService addressService,
        IUnitOfWork uow)
    {
        _addressService = addressService;
        _uow = uow;
    }

    #endregion

    public List<ShowAddressInProfileViewModel> Addresses { get; set; }

    public async Task OnGet()
    {
        Addresses = await _addressService.GetAllUserAddresses();
    }

    /// <summary>
    /// حذف آدرس
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> OnPostDeleteAddress(long id)
    {
        // آیا کاربر داخل سیستم همچین آدرسی با آیدی ورودی دارد یا خیر ؟
        // اگر داشته باشد، آدرس را حذف میکنیم در غیر اینصورت به کاربر خطا نمایش میدهیم
        var result = await _addressService.RemoveUserAddress(id);

        if (!result)
        {
            return JsonBadRequest(PublicConstantStrings.RecordNotFoundMessage);
        }

        await _uow.SaveChangesAsync();

        return JsonOk("آدرس مورد نظر با موفقیت حذف شد");
    }
}