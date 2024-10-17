using Microsoft.AspNetCore.Mvc;

namespace WebAPI.services;

public interface IFileService
{
    Task<string> UploadFile(IFormFile file);
    Task<string> UpdateFile(IFormFile file, string fileName );
    Task<bool> DeleteFile(string fileName);
    Task<FileResult> GetFile(string fileName);

}