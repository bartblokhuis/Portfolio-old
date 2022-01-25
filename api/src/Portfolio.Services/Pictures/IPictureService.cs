using Microsoft.AspNetCore.Http;
using Portfolio.Domain.Models;

namespace Portfolio.Services.Pictures;

public interface IPictureService
{
    Task<IEnumerable<Picture>> GetAllAsync();

    Task<Picture> GetByIdAsync(int projectId);

    Task<Picture> CreatePictureFromFileAsync(IFormFile file, string titleAttribute, string altAttribute);

    Task<Picture> UpdatePictureFromFileAsync(Picture picture, IFormFile file, string titleAttribute, string altAttribute);

    Task<string> DeleteAsync(Picture picture);
}
