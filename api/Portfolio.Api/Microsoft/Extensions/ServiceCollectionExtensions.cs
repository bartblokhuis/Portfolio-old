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

namespace Portfolio.Microsoft.Extensions
{
    public static class ServiceCollectionExtensions
    {
        #region Methods

        public static void AddPortfolioServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();

            services.AddSettings(configuration);
            services.AddDatabases(configuration);

            services.AddRepository();
            services.AddAppServices();

            services.AddAuthentication(configuration);

            services.AddOpenApi();

            services.AddAutoMapper(typeof(PortfolioMappings));
            services.AddCors((options => { options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin()); }));

        }

        private static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<,,>), typeof(BaseRepository<,,>));
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(ISettingService<>), typeof(SettingsService<>));
        }

        private static void AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IUploadImageHelper, UploadImageHelper>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ISkillGroupService, SkillGroupService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IAboutMeService, AboutMeService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddSingleton(new HostingConfig());
            services.AddScoped<IWebHelper, WebHelper>();
        }

        private static void AddSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettings = new AppSettings();
            configuration.Bind(appSettings);
            services.AddSingleton(appSettings);
        }

        private static void AddDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase<PortfolioContext>(configuration["ConnectionString:DefaultConnection"]);
            services.AddDatabase<AuthenticationDbContext>(configuration["ConnectionString:DefaultConnection"]);
        }

        private static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
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
        }

        private static void AddOpenApi(this IServiceCollection services)
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
        }

        #endregion

        #region Utils

        private static void AddDatabase<TContext>(this IServiceCollection services, string connectionString) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(connectionString, e => e.MigrationsAssembly(typeof(TContext).Assembly.FullName));
            });

            //Apply new migrations
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
            dbContext.Database.Migrate();
        }

        #endregion
    }
}
