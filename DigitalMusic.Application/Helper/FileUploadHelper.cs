using DigitalMusic.Application.Helper.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace DigitalMusic.Application.Helper;

public class FileUploadHelper  : IFileUploadHelper
{
    private readonly IWebHostEnvironment _environment;
    public FileUploadHelper(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> SaveFileAsync(IFormFile imageFile, string[] allowFileExtention)
    {
        if (imageFile is null)
        {
            throw new ArgumentNullException(nameof(imageFile));
        }

        var contentPath = _environment.WebRootPath;
        var path = Path.Combine(contentPath, "uploads");

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var ext = Path.GetExtension(imageFile.FileName);
        if (!allowFileExtention.Contains(ext))
        {
            throw new ArgumentException($"Only {string.Join(",", allowFileExtention)} are allowed.");
        }

        var fileName = $"{Guid.NewGuid().ToString()}{ext}";
        var fileNameWithPath = Path.Combine(path, fileName);
        using var stream = new FileStream(fileNameWithPath, FileMode.Create);
        await imageFile.CopyToAsync(stream);
        return fileName;
    }

    public void DeleteFile(string fileNameWithExtention)
    {
        if (string.IsNullOrEmpty(fileNameWithExtention))
        {
            throw new ArgumentNullException(nameof(fileNameWithExtention));
        }

        var contentPath = _environment.WebRootPath;
        var path = Path.Combine(contentPath, "uploads", fileNameWithExtention);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Invalid file path");
        }
        
        File.Delete(path);
    }
}