namespace CurfewService;

public class WindowsBackgroundService : BackgroundService
{
    private readonly ILogger<WindowsBackgroundService> _logger;
    private readonly ShutdownService _shutdownService;

    public WindowsBackgroundService(
        ILogger<WindowsBackgroundService> logger,
        ShutdownService shutdownService)
    {
        _shutdownService = shutdownService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try 
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _shutdownService.EvaluateShutdown();
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{message}", ex.Message);

            Environment.Exit(1);
        }
    }
}
