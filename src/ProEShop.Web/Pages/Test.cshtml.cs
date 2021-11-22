using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProEShop.DataLayer.Context;

namespace ProEShop.Web.Pages;

[Authorize]
public class TestModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public TestModel(ApplicationDbContext context)
    {
        _context = context;
    }
    public void OnGet()
    {
        var products = _context.Prouducts.Where(x => x.Category.Parent.ParentId == 1);
    }
}
