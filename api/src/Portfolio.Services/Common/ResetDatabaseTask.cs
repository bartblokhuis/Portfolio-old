using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Caching;
using Portfolio.Core.Configuration;
using Portfolio.Core.Infrastructure;
using Portfolio.Database;
using Portfolio.Domain.Models.Settings;
using Portfolio.Services.Settings;
using Portfolio.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.Common;

internal class ResetDatabaseTask : IScheduleTask
{
    #region Fields

    private readonly PortfolioContext _context;
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IScheduleTaskService _scheduleTaskService;
    private readonly ISettingService<ApiSettings> _apiSettings;
    private readonly AppSettings _appSettings;

    #endregion

    #region Constructor

    public ResetDatabaseTask(PortfolioContext context, IStaticCacheManager staticCacheManager, ISettingService<ApiSettings> apiSettings, IScheduleTaskService scheduleTaskService, AppSettings appSettings)
    {
        _context = context;
        _staticCacheManager = staticCacheManager;
        _scheduleTaskService = scheduleTaskService;
        _apiSettings = apiSettings;
        _appSettings = appSettings;
    }

    #endregion

    public async Task ExecuteAsync()
    {
        //Reseting the database should only be possible for the demo application
        if (!_appSettings.IsDemo)
            return;

        var apiSettings = await _apiSettings.GetAsync();

        //Reset the database to it's original state
        var context = EngineContext.Current.Resolve<PortfolioContext>();
        context.ChangeTracker
            .Entries()
            .ToList()
            .ForEach(e => e.State = EntityState.Detached);

        await context.Database.EnsureDeletedAsync();
        await context.Database.MigrateAsync();

        await _staticCacheManager.ClearAsync();

        await _apiSettings.SaveAsync(apiSettings);
        await _scheduleTaskService.InsertTaskAsync(new Domain.Models.ScheduleTask
        {
            Enabled = true,
            Name = "Clear database",
            Seconds = 120,
            StopOnError = false,
            Type = "Portfolio.Services.Common.ResetDatabaseTask, Portfolio.Services",
        });


    }
}
