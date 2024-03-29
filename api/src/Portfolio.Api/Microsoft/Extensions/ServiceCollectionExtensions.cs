﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Portfolio.Core.AutoMapper;
using Portfolio.Core.Caching;
using Portfolio.Core.Configuration;
using Portfolio.Core.Events;
using Portfolio.Core.Helpers;
using Portfolio.Core.Infrastructure;
using Portfolio.Database;
using Portfolio.Domain.Models.Authentication;
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
using Portfolio.Services.UserPreference;
using Portfolio.Services.Users;
using Portfolio.Services.WorkContexts;
using System.Text;

namespace Portfolio.Microsoft.Extensions;

public static class ServiceCollectionExtensions
{
    #region Methods

    public static void AddPortfolioServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        CommonHelper.DefaultFileProvider = new PortfolioFileProvider(webHostEnvironment);

        services.AddControllers();
        services.AddMvc();
        services.AddHttpContextAccessor()
            .AddFileProvider();

        services.AddHttpClient("ScheduleTask");

        services.AddSettings(configuration)
            .AddDatabases(configuration)
            .AddCache()
            .AddRepository()
            .AddAppServices()
            .AddAuthentication(configuration)
            .AddOpenApi()
            .AddEventPublisher()
            .AddPortfolioAutoMapper()
            .AddCors((options => { options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()); }));

        services.AddEngine(configuration);

    }

    private static IServiceCollection AddPortfolioAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(PortfolioMappings).Assembly);
        return services;
    }

    private static IServiceCollection AddCache(this IServiceCollection services)
    {
        services.AddMemoryCache()
            .AddSingleton<ILocker, MemoryCacheManager>()
            .AddSingleton<IStaticCacheManager, MemoryCacheManager>();
        return services;
    }

    private static IServiceCollection AddEngine(this IServiceCollection services, IConfiguration configuration)
    {
        var engine = EngineContext.Create();
        engine.ConfigureServices(services, configuration);

        return services;
    }

    private static IServiceCollection AddFileProvider(this IServiceCollection services)
    {
        services.AddScoped<IPortfolioFileProvider, PortfolioFileProvider>();

        return services;
    }

    private static IServiceCollection AddEventPublisher(this IServiceCollection services)
    {
        services.AddScoped<IEventPublisher, EventPublisher>();

        return services;
    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<,,>), typeof(BaseRepository<,,>))
            .AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>))
            .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
            .AddScoped(typeof(ISettingService<>), typeof(SettingsService<>));

        return services;
    }

    private static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddSingleton<IUploadImageHelper, UploadImageHelper>()
            .AddScoped<IMessageService, MessageService>()
            .AddScoped<ISkillGroupService, SkillGroupService>()
            .AddScoped<ISkillService, SkillService>()
            .AddScoped<IAboutMeService, AboutMeService>()
            .AddScoped<IProjectService, ProjectService>()
            .AddScoped<IScheduleTaskService, ScheduleTaskService>()
            .AddScoped<IUrlService, UrlService>()
            .AddScoped<IBlogPostService, BlogPostService>()
            .AddScoped<IBlogPostCommentService, BlogPostCommentService>()
            .AddScoped<IBlogSubscriberService, BlogSubscriberService>()
            .AddSingleton(new HostingConfig())
            .AddScoped<IWebHelper, WebHelper>()
            .AddScoped<IQueuedEmailService, QueuedEmailService>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<ITokenizer, Tokenizer>()
            .AddScoped<IMessageTokenProvider, MessageTokenProvider>()
            .AddScoped<ILanguageService, LanguageService>()
            .AddScoped<IPictureService, PictureService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IUserPreferencesService, UserPreferencesService>()
            .AddScoped<IWorkContext, WorkContext>();

        return services;
    }

    private static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = new AppSettings();
        configuration.Bind(appSettings);
        services.AddSingleton(appSettings);

        return services;
    }

    private static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase<PortfolioContext>(configuration["ConnectionString:DefaultConnection"])
            .AddDatabase<AuthenticationDbContext>(configuration["ConnectionString:IdentityConnection"]);

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // For Identity  
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthenticationDbContext>()
            .AddDefaultTokenProviders();

        // Adding Authentication  
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        // Adding Jwt Bearer  
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration["JWT:ValidAudience"],
                ValidIssuer = configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
            };
        });

        return services;
    }

    private static IServiceCollection AddOpenApi(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Bart's Portfolio API", Version = "v1" });

            //Adds the jwt header required for authorization.
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {securityScheme, new string[] { }}
            });
        });

        return services;
    }

    #endregion

    #region Utils

    private static IServiceCollection AddDatabase<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
        {
            options.UseSqlServer(connectionString, e => e.MigrationsAssembly(typeof(TContext).Assembly.FullName));
        });

        //Apply new migrations
        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
        dbContext.Database.Migrate();

        return services;
    }

    #endregion
}
