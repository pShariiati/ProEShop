using Microsoft.AspNetCore.Http;

namespace ProEShop.Common.Helpers;

public static class FileHelpers
{
    public static bool IsFileUploaded(this IFormFile file)
    {
        return file is { Length: > 0 };
    }
}
