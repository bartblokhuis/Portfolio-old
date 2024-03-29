using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Portfolio.Core.Infrastructure;
using Portfolio.Microsoft.Extensions;

namespace Portfolio;

public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        Configuration = configuration;
        _webHostEnvironment = webHostEnvironment;
    }

    public IConfiguration Configuration { get; set; }
    private readonly IWebHostEnvironment _webHostEnvironment;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddPortfolioServices(Configuration, _webHostEnvironment);
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //Start engine
        var engine = EngineContext.Current;

        app.UseStatusCodePagesWithReExecute("/Error", "?status={0}");
        app.UsePortfolio(env);


        app.UseExceptionHandler(a => a.Run(async context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature.Error;

            await context.Response.WriteAsJsonAsync(new { error = exception.Message });
        }));

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials()); // allow credentials

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
