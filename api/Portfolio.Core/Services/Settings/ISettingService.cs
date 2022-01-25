﻿using Portfolio.Domain.Models.Common;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Settings;

public interface ISettingService<T> where T : BaseEntity, ISetting
{
    Task<T> GetAsync();

    Task SaveAsync(T settings);
}
