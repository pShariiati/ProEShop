using Microsoft.AspNetCore.Http;

namespace ProEShop.Services.Contracts;

public interface IUploadFileService
{
    Task SaveFile(IFormFile file, string fileName, params string[] destinationDirectoryNames);
}