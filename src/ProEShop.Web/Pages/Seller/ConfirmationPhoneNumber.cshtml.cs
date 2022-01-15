using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProEShop.Web.Pages.Seller;

public class ConfirmationPhoneNumberModel : PageModel
{
    [ViewData]
    public string PhoneNumber { get; set; }

    public void OnGet(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }
}