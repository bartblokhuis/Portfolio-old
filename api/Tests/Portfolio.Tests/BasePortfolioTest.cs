using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Moq;
using Portfolio.Core.Caching;
using Portfolio.Core.Configuration;
using Portfolio.Core.Events;
using Portfolio.Core.Infrastructure;
using Portfolio.Database;
using Portfolio.Services.AboutMeServices;
using Portfolio.Services.Blogs;
using Portfolio.Services.BlogSubscribers;
using Portfolio.Services.Comments;
using Portfolio.Services.Common;
using Portfolio.Services.Languages;
using Portfolio.Services.Messages;
using Portfolio.Services.Pictures;
using Portfolio.Services.Projects;
using Portfolio.Services.QueuedEmails;
using Portfolio.Services.Repository;
using Portfolio.Services.Settings;
using Portfolio.Services.SkillGroups;
using Portfolio.Services.Skills;
using Portfolio.Services.Tasks;
using Portfolio.Services.Tokens;
using Portfolio.Services.Urls;
using System;

namespace Portfolio.Tests;

public abstract class BasePortfolioTest
{

    private static readonly ServiceProvider _serviceProvider;

    static BasePortfolioTest()
    {
        var services = new ServiceCollection();

        services.AddHttpClient();

        var memoryCache = new MemoryCache(new MemoryCacheOptions());
        var typeFinder = new AppDomainTypeFinder();

        var httpContext = new DefaultHttpContext
        {
            Request = { Headers = { { HeaderNames.Host, "127.0.0.1" } } }
        };

        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor.Setup(p => p.HttpContext).Returns(httpContext);

        services.AddSingleton(httpContextAccessor.Object);

        services.AddSingleton<IMemoryCache>(memoryCache);
        services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();
        services.AddSingleton<ILocker, MemoryCacheManager>();

        services.AddDbContext<PortfolioContext>((builder) =>
        {
            builder.UseInMemoryDatabase("Portfolio");
        }, ServiceLifetime.Transient);

        services.AddTransient<IEventPublisher, EventPublisher>();

        services.AddTransient(typeof(IBaseRepository<,>), typeof(BaseRepository<,>))
            .AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddTransient(typeof(ISettingService<>), typeof(SettingsService<>));

        services.AddTransient<IUploadImageHelper, UploadImageHelper>()
            .AddTransient<IMessageService, MessageService>()
            .AddTransient<ISkillGroupService, SkillGroupService>()
            .AddTransient<ISkillService, SkillService>()
            .AddTransient<IAboutMeService, AboutMeService>()
            .AddTransient<IProjectService, ProjectService>()
            .AddTransient<IScheduleTaskService, ScheduleTaskService>()
            .AddTransient<IUrlService, UrlService>()
            .AddTransient<IBlogPostService, BlogPostService>()
            .AddTransient<IBlogPostCommentService, BlogPostCommentService>()
            .AddTransient<IBlogSubscriberService, BlogSubscriberService>()
            .AddSingleton(new HostingConfig())
            .AddTransient<IWebHelper, WebHelper>()
            .AddTransient<IQueuedEmailService, QueuedEmailService>()
            .AddTransient<IEmailService, EmailService>()
            .AddTransient<ITokenizer, Tokenizer>()
            .AddTransient<IMessageTokenProvider, MessageTokenProvider>()
            .AddTransient<ILanguageService, LanguageService>()
            .AddTransient<IPictureService, PictureService>();

        _serviceProvider = services.BuildServiceProvider();

        EngineContext.Replace(new PortfolioTestEngine(_serviceProvider));
    }

    public T GetService<T>()
    {
        try
        {
            return _serviceProvider.GetRequiredService<T>();
        }
        catch (InvalidOperationException)
        {
            return (T)EngineContext.Current.ResolveUnregistered(typeof(T));
        }
    }

    public partial class PortfolioTestEngine : PortfolioEngine
    {
        protected readonly IServiceProvider _internalServiceProvider;

        public PortfolioTestEngine(IServiceProvider serviceProvider)
        {
            _internalServiceProvider = serviceProvider;
        }

        public override IServiceProvider ServiceProvider => _internalServiceProvider;
    }
}
