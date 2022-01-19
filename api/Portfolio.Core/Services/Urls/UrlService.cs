﻿using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Urls;

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

    #endregion
}
