using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProEShop.Services.Contracts;

namespace ProEShop.Services.Services;

public class UploadFileService : IUploadFileService
{
    private const int MaxBufferSize = 0x10000; // 64K. The artificial constraint due to win32 api limitations. Increasing the buffer size beyond 64k will not help in any circumstance, as the underlying SMB protocol does not support buffer lengths beyond 64k.

    private readonly IWebHostEnvironment _environment;

    public UploadFileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task SaveFile(IFormFile file, string fileName, string oldFileName, params string[] destinationDirectoryNames)
    {
        if (file == null || file.Length == 0)
        {
            return;
        }

        var uploadsRootFolder = Path.Combine(_environment.WebRootPath);

        if (destinationDirectoryNames is not null)
        {
            foreach (var folderName in destinationDirectoryNames)
            {
                uploadsRootFolder = Path.Combine(uploadsRootFolder, folderName);
            }
        }

        if (!Directory.Exists(uploadsRootFolder))
        {
            Directory.CreateDirectory(uploadsRootFolder);
        }

        var filePath = Path.Combine(uploadsRootFolder, fileName);
        if (oldFileName != null)
        {
            var oldFilePath = Path.Combine(uploadsRootFolder, oldFileName);
            File.Delete(oldFilePath);
        }

        await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write,
            FileShare.None,
            MaxBufferSize,
            // you have to explicitly open the FileStream as asynchronous
            // or else you're just doing synchronous operations on a background thread.
            useAsync: true);
        await file.CopyToAsync(fileStream);
    }
}
