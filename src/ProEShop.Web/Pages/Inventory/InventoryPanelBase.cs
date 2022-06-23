using Microsoft.AspNetCore.Authorization;
using ProEShop.Services.Services.Identity;

namespace ProEShop.Web.Pages.Inventory;

[Authorize(Roles = ConstantRoles.Warehouse)]
public class InventoryPanelBase : PageBase
{

}