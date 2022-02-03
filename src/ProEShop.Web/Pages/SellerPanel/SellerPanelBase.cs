using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.Services.Services.Identity;

namespace ProEShop.Web.Pages.SellerPanel;

[Authorize(Roles = ConstantRoles.Seller)]
public class SellerPanelBase : PageBase
{

}