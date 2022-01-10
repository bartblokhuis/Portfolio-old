using Microsoft.AspNetCore.Http;
using Portfolio.Domain.Models;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces;

public interface IPictureService
{
    Task<Picture> GetById(int projectId);

    Task<Picture> CreatePictureFromFile(IFormFile file, string titleAttribute, string altAttribute);

    Task<Picture> UpdatePictureFromFile(Picture picture, IFormFile file, string titleAttribute, string altAttribute);
}
