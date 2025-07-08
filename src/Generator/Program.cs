using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Generator;
using System.IO.Abstractions;

IHost host = Host.CreateDefaultBuilder(args)
    .UseSerilog((context, services, configuration) =>
        configuration.WriteTo.Console())
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IFileSystem, FileSystem>();
        services.AddSingleton(TimeProvider.System);
        services.AddSingleton<FileGenerator>();
        services.AddHostedService<PlaceholderService>();
    })
    .Build();

await host.RunAsync();
