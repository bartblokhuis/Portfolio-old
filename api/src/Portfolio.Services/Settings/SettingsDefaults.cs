﻿using Portfolio.Core.Caching;

namespace Portfolio.Services.Settings;
public class SettingsDefaults
{
    public static string SettingsPrefix => "Portfolio.settings.";

    public static CacheKey SettingsCacheKey => new CacheKey("Portfolio.settings.", SettingsPrefix);
}

