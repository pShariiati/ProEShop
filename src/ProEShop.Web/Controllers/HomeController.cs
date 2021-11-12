using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProEShop.DataLayer.Context;
using ProEShop.Entities;
using ProEShop.Services.Contracts;

namespace ProEShop.Web.Controllers;
public class HomeController : Controller
{
    private readonly IUnitOfWork _uow;
    private readonly ICategoryService _categoryService;

    public HomeController(
        IUnitOfWork uow,
        ICategoryService categoryService
    )
    {
        _uow = uow;
        _categoryService = categoryService;
    }
    public async Task<IActionResult> Index()
    {
        await _categoryService.AddAsync(new Category()
        {
            Test = "test",
            Title = "test2"
        });
        await _uow.SaveChangesAsync();
        var categories = await _categoryService.GetAll();
        return View();
    }
}