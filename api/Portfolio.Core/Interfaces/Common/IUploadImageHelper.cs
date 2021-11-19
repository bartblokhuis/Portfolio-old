using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Common
{
    public interface IUploadImageHelper
    {
        string ValidateImage(IFormFile image);

        Task<string> UploadImage(IFormFile image);
    }
}
