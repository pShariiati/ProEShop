using Microsoft.AspNetCore.Authorization;
using ProEShop.Services.Services.Identity;

namespace ProEShop.Web.Pages.Inventory;

[Authorize(Roles = $"{ConstantRoles.Warehouse},{ConstantRoles.DeliveryMan}")]
public class InventoryPanelBase : PageBase
{

}