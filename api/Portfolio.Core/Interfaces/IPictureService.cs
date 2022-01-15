using Microsoft.AspNetCore.Http;
using Portfolio.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Portfolio.Core.Interfaces;

public interface IPictureService
{
    Task<IEnumerable<Picture>> GetAll();

    Task<Picture> GetById(int projectId);

    Task<Picture> CreatePictureFromFile(IFormFile file, string titleAttribute, string altAttribute);

    Task<Picture> UpdatePictureFromFile(Picture picture, IFormFile file, string titleAttribute, string altAttribute);

    Task<string> Delete(Picture picture);
}
