using Microsoft.AspNetCore.Http;

namespace ProEShop.Services.Contracts;

public interface IUploadFileService
{
    Task SaveFile(IFormFile file, string fileName, string oldFileName, params string[] destinationDirectoryNames);
    void DeleteFile(string fileName, params string[] destinationDirectoryNames);
}