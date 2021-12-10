using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Portfolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                config.SetBasePath(PathExtensions.GetApplicationBasePath());
                config.AddJsonFile("appsettings.json", false);
            }).ConfigureWebHostDefaults(webBuilder => { webBuilder.UseContentRoot(PathExtensions.GetApplicationBasePath()); webBuilder.UseStartup<Startup>(); });
    }
}
