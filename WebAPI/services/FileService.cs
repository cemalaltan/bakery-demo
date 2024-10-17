using Microsoft.AspNetCore.Mvc;

namespace WebAPI.services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _env;
    private const long MaxFileSize = 3 * 1024 * 1024;
    private const string FolderName = "SourceFiles";

  public  FileService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("No file uploaded");

        if (file.Length > MaxFileSize)
            throw new ArgumentException($"File size exceeds the maximum limit of {MaxFileSize / (1024 * 1024)} MB");

        var uploadsFolder = Path.Combine(_env.ContentRootPath, "SourceFiles");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = GenerateUniqueFileName(file.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return uniqueFileName;
    }

    public async Task<string> UpdateFile(IFormFile file, string fileName)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("No file uploaded");

        if (file.Length > MaxFileSize)
            throw new ArgumentException($"File size exceeds the maximum limit of {MaxFileSize / (1024 * 1024)} MB");

        var uploadsFolder = Path.Combine(_env.ContentRootPath, FolderName);
        var oldFilePath = Path.Combine(uploadsFolder, fileName);

        if (!System.IO.File.Exists(oldFilePath))
        {
            throw new FileNotFoundException("File not found");
        }

        System.IO.File.Delete(oldFilePath);

        var uniqueFileName = GenerateUniqueFileName(file.FileName);
        var newFilePath = Path.Combine(uploadsFolder, uniqueFileName);
        using (var stream = new FileStream(newFilePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return uniqueFileName;
    }

    public async Task<bool> DeleteFile(string fileName)
    {
        var uploadsFolder = Path.Combine(_env.ContentRootPath, FolderName);
        var filePath = Path.Combine(uploadsFolder, fileName);

        if (!System.IO.File.Exists(filePath))
        {
            return false;
        }

        System.IO.File.Delete(filePath);

        return true;
    }

    public async Task<FileResult> GetFile(string fileName)
    {
        var uploadsFolder = Path.Combine(_env.ContentRootPath, FolderName);
        var filePath = Path.Combine(uploadsFolder, fileName);

        if (!System.IO.File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found");
        }

        var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        var contentType = "application/octet-stream"; // You can use a library to determine the correct MIME type

        return new FileContentResult(fileBytes, contentType)
        {
            FileDownloadName = fileName
        };
    }
    
    private string GenerateUniqueFileName(string originalFileName)
    {
        var fileExtension = Path.GetExtension(originalFileName);
        var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
        return uniqueFileName;
    }
}