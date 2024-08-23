using Microsoft.AspNetCore.Http;

namespace DigitalMusic.Application.Helper.Interface;

public interface IFileUploadHelper
{
    public Task<string> SaveFileAsync(IFormFile imageFile, string[] allowFileExtention);
    public void DeleteFile(string fileNameWithExtention);
}