namespace ProEShop.Services.Contracts;

public interface IViewRendererService
{
    /// <summary>
    /// Renders a .cshtml file as an string.
    /// </summary>
    Task<string> RenderViewToStringAsync(string viewNameOrPath);

    /// <summary>
    /// Renders a .cshtml file as an string.
    /// </summary>
    Task<string> RenderViewToStringAsync<TModel>(string viewNameOrPath, TModel model);
}