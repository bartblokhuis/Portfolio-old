using Microsoft.AspNetCore.Http;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Common;

namespace Portfolio.Services.Pictures;

public interface IPictureService
{
    Task<IEnumerable<Picture>> GetAllAsync();

    Task<IPagedList<Picture>> GetAllPicturesAsync(int pageIndex = 0, int pageSize = int.MaxValue);

    Task<Picture> GetByIdAsync(int projectId);

    Task<Picture> CreatePictureFromFileAsync(IFormFile file, string titleAttribute, string altAttribute);

    Task<Picture> UpdatePictureFromFileAsync(Picture picture, IFormFile file, string titleAttribute, string altAttribute);

    Task<string> DeleteAsync(Picture picture);
}
