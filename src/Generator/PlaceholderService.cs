using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO.Abstractions;

public class PlaceholderService : BackgroundService
{
    private readonly ILogger<PlaceholderService> _logger;
    private readonly TimeProvider _timeProvider;
    private readonly IFileSystem _fileSystem;

    public PlaceholderService(ILogger<PlaceholderService> logger, TimeProvider timeProvider, IFileSystem fileSystem)
    {
        _logger = logger;
        _timeProvider = timeProvider;
        _fileSystem = fileSystem;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Service running at {Time}", _timeProvider.GetUtcNow());
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}
