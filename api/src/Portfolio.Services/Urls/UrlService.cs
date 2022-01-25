using Portfolio.Domain.Models;
using Portfolio.Services.Repository;

namespace Portfolio.Services.Urls;

public class UrlService : IUrlService
{
    #region Properties

    private readonly IBaseRepository<Url> _urlRepository;

    #endregion

    #region Constructor

    public UrlService(IBaseRepository<Url> urlRepository)
    {
        _urlRepository = urlRepository;
    }

    #endregion

    #region Methods

    public Task DeleteAsync(int id)
    {
        return _urlRepository.DeleteAsync(id);
    }

    public Task UpdateAsync(Url url)
    {
        return _urlRepository.UpdateAsync(url);
    }

    #endregion
}

