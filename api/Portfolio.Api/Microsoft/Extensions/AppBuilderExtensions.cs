using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Portfolio.Microsoft.Extensions;

public static class AppBuilderExtensions
{
    #region Methods
    public static void UsePortfolio(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseOpenApi();
        app.UsePortfolioOpenFiles(env);
    }

    private static void UseOpenApi(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bart's Portfolio API");
        });
    }

    private static void UsePortfolioOpenFiles(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        var path = Path.Combine(env.ContentRootPath, string.Format("wwwroot{0}uploads", Path.DirectorySeparatorChar));
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(path),
            RequestPath = string.Format("/uploads")
        });
    }

    #endregion
}
