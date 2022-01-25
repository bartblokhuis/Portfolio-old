using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Portfolio.Services.Common;

public interface IUploadImageHelper
{
    string ValidateImage(IFormFile image);

    Task<string> UploadImageAsync(IFormFile image);

    void DeleteImage(string path);
}
