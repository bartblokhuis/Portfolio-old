using Microsoft.EntityFrameworkCore;
using Portfolio.Core.Caching;
using Portfolio.Core.Configuration;
using Portfolio.Core.Infrastructure;
using Portfolio.Database;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Models.Settings;
using Portfolio.Services.Blogs;
using Portfolio.Services.BlogSubscribers;
using Portfolio.Services.Settings;
using Portfolio.Services.SkillGroups;
using Portfolio.Services.Skills;
using Portfolio.Services.Tasks;

namespace Portfolio.Services.Common;

internal class ResetDatabaseTask : IScheduleTask
{
    #region Fields

    private readonly PortfolioContext _context;
    private readonly IStaticCacheManager _staticCacheManager;
    private readonly IScheduleTaskService _scheduleTaskService;
    private readonly ISettingService<ApiSettings> _apiSettings;
    private readonly AppSettings _appSettings;
    private readonly ISkillGroupService _skillGroupService;
    private readonly ISkillService _skillService;
    private readonly IBlogSubscriberService _blogSubscriberService;
    private readonly IBlogPostService _blogPostService;
    #endregion

    #region Constructor

    public ResetDatabaseTask(PortfolioContext context, IStaticCacheManager staticCacheManager, ISettingService<ApiSettings> apiSettings, IScheduleTaskService scheduleTaskService, AppSettings appSettings, ISkillGroupService skillGroupService, ISkillService skillService, IBlogSubscriberService blogSubscriberService, IBlogPostService blogPostService)
    {
        _context = context;
        _staticCacheManager = staticCacheManager;
        _scheduleTaskService = scheduleTaskService;
        _apiSettings = apiSettings;
        _appSettings = appSettings;
        _skillGroupService = skillGroupService;
        _skillService = skillService;
        _blogSubscriberService = blogSubscriberService;
        _blogPostService = blogPostService;
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

        //Data seeding
        await _scheduleTaskService.InsertTaskAsync(new ScheduleTask
        {
            Enabled = true,
            Name = "Clear database",
            Seconds = 120,
            StopOnError = false,
            Type = "Portfolio.Services.Common.ResetDatabaseTask, Portfolio.Services",
        });

        var frontEnd = new SkillGroup
        {
            DisplayNumber = 0,
            Title = "Front End"
        };
        await _skillGroupService.InsertAsync(frontEnd);

        var backEnd = new SkillGroup
        {
            DisplayNumber = 0,
            Title = "Back End"
        };
        await _skillGroupService.InsertAsync(backEnd);

        var css = new Skill
        {
            Name = "CSS",
            IconPath = "uploads/ffghphhg.hnb.svg",
            SkillGroupId = frontEnd.Id
        };
        await _skillService.InsertAsync(css);

        var html = new Skill
        {
            Name = "HTML",
            IconPath = "uploads/v1rhnnr3.35w.svg",
            SkillGroupId = frontEnd.Id
        };
        await _skillService.InsertAsync(html);

        var csharp = new Skill
        {
            Name = "C#",
            IconPath = "uploads/5lapiows.s0v.svg",
            SkillGroupId = backEnd.Id
        };
        await _skillService.InsertAsync(csharp);

        await _blogSubscriberService.SubscribeAsync(
            new BlogSubscriber
            {
                EmailAddress = "Ethan_Murray5942@brety.org",
                CreatedAtUTC = DateTime.UtcNow.AddDays(-2)
            });

        await _blogSubscriberService.SubscribeAsync(
            new BlogSubscriber
            {
                EmailAddress = "Carla_Reid1285@grannar.com",
                CreatedAtUTC = DateTime.UtcNow.AddDays(-1)
            });

        await _blogSubscriberService.SubscribeAsync(
            new BlogSubscriber
            {
                EmailAddress = "Alessandra_Young9634@bretoux.com",
                CreatedAtUTC = DateTime.UtcNow.AddDays(-1)
            });

        await _blogSubscriberService.SubscribeAsync(
            new BlogSubscriber
            {
                EmailAddress = "Bryon_Alexander1311@gembat.biz",
            });

        await _blogPostService.InsertAsync(new BlogPost
        {
            Title = "My first blog post!",
            Description = "This is my first blog post",
            Content = "Welcome to my blog",
            IsPublished = true,
            DisplayNumber = 0
        });

        var keepAliveTask = await _scheduleTaskService.GetTaskByTypeAsync("Portfolio.Services.Common.KeepAliveTask, Portfolio.Services");
        if(keepAliveTask != null)
        {
            keepAliveTask.Enabled = false;
            await _scheduleTaskService.UpdateTaskAsync(keepAliveTask);
        }

        var queuedMessagesTask = await _scheduleTaskService.GetTaskByTypeAsync("Portfolio.Services.Common.QueuedMessagesSendTask, Portfolio.Services");
        if (queuedMessagesTask != null)
        {
            queuedMessagesTask.Enabled = false;
            await _scheduleTaskService.UpdateTaskAsync(queuedMessagesTask);
        }
    }
}
