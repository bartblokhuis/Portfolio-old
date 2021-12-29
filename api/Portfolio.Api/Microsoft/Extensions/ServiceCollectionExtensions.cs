using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Portfolio.Core.AutoMapper;
using Portfolio.Core.Configuration;
using Portfolio.Core.Interfaces;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Core.Services;
using Portfolio.Core.Services.Common;
using Portfolio.Database;
using Portfolio.Domain.Models.Authentication;
using System.Text;

namespace Portfolio.Microsoft.Extensions;

public static class ServiceCollectionExtensions
{
    #region Methods

    public static void AddPortfolioServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddHttpContextAccessor();

        services.AddSettings(configuration)
            .AddDatabases(configuration)
            .AddRepository()
            .AddCache()
            .AddAppServices()
            .AddAuthentication(configuration)
            .AddOpenApi()
            .AddAutoMapper(typeof(PortfolioMappings))
            .AddAutoMapper(typeof(BlogProfile))
            .AddCors((options => { options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()); }));

    }

    private static IServiceCollection AddCache(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<CacheService>();
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
            .AddScoped<IBlogService, BlogService>()
            .AddSingleton(new HostingConfig())
            .AddScoped<IWebHelper, WebHelper>();
            .AddScoped<IEmailService, EmailService>();

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
            .AddDatabase<AuthenticationDbContext>(configuration["ConnectionString:DefaultConnection"]);

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
