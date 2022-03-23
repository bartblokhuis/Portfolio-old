using Microsoft.AspNetCore.Http;

namespace Portfolio.Services.Common;

public interface IUploadImageHelper
{
    string ValidateImage(IFormFile image);

    Task<string> UploadImageAsync(IFormFile image);

    Task<string> UploadLanguageFlagImageAsync(IFormFile image);

    void DeleteImage(string path);
}
