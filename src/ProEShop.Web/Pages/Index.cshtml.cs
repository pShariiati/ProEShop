using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProEShop.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger=logger;
        }

        public void OnGet()
        {
            _logger.LogError("Error log...");
            _logger.LogError("Error log1...");
            _logger.LogError("Error log2...");
        }
    }
}
