using System;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;


namespace Generator
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
            // Safely get the directory of the executable
            var exeDir = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName)!;


            IHostBuilder builder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config
                        .SetBasePath(exeDir)
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                        .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false)
                        .AddEnvironmentVariables();
                });
            
            builder
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));
                    services.AddSingleton(TimeProvider.System);
                    services.AddSingleton<IFileSystem, FileSystem>();
                    services.AddHostedService<HelloWorker>();
                    // Register your services here
                });

            builder.UseEnvironment(environment);

            ConfigureLogging(builder);

            IHost app = builder.Build();

            Log.Debug("Starting application in {Environment} environment", environment);
            await app.RunAsync();

        }

        /// <summary>
        /// Configures Serilog
        /// </summary>
        /// <param name="builder"></param>
        private static void ConfigureLogging(IHostBuilder builder)
        {
            builder.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.WithProperty("EnvMachineName", Environment.MachineName)
                    .Enrich.WithProperty("EnvUserName", Environment.UserName)
                    .Enrich.FromLogContext()
                    .Enrich.WithAssemblyName()
                    .Enrich.WithAssemblyVersion();
            });
        }
    }
}
