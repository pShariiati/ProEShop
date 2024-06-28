using Microsoft.AspNetCore.Authorization;
using ProEShop.Services.Services.Identity;

namespace ProEShop.Web.Pages.SellerPanel;

[Authorize(Roles = ConstantRoles.Seller)]
public class SellerPanelBase : PageBase
{

}