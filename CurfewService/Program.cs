using CurfewService;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options => {
        options.ServiceName = "Custom Curfew Service";
    })
    .ConfigureServices(services =>
    {
        services.AddSingleton<ShutdownService>();
        services.AddHostedService<WindowsBackgroundService>();
    })
    .Build();

await host.RunAsync();
