using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Generator;
internal class HelloWorker : BackgroundService
{
    private readonly ILogger<HelloWorker> _logger;
    private readonly AppSettings _settings;
    private readonly TimeProvider _timeProvider;
    private readonly IFileSystem _fileSystem;

    public HelloWorker(
        ILogger<HelloWorker> logger, 
        IOptions<AppSettings> settings,
        TimeProvider timeProvider,
        IFileSystem fileSystem)
    {
        _logger = logger;
        _settings = settings.Value;
        _timeProvider = timeProvider;
        _fileSystem = fileSystem;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

            string message;
            var settings = _settings;
            
            message = string.IsNullOrEmpty(settings.UserName)
                && string.IsNullOrWhiteSpace(settings.MessageWithUser)
                ? settings.MessageNoUser
                : settings.MessageWithUser.Replace("{UserName}", settings.UserName);

            _logger.LogInformation("Greeting at {Time}: {Message}", 
                _timeProvider.GetLocalNow(), message);
            
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
