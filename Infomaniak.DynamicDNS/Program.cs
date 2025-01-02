using Infomaniak.DynamicDNS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

public class Program
{
    private static CancellationTokenSource _cancellationTokenSource = new();

    public static async Task Main(string[] args)
    {
        AppDomain.CurrentDomain.ProcessExit += new EventHandler((sender, e) => _cancellationTokenSource.Cancel());
        var serviceProvider = Init();

        var client = serviceProvider.GetRequiredService<DynamicDnsClient>();
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        var interval = TimeSpan.FromMinutes(client.Settings.Interval);

        if (interval <= TimeSpan.Zero)
        {
            logger.LogInformation("The interval must be greater than zero.");
            return;
        }

        var cancellationToken = _cancellationTokenSource.Token;
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var response = await client.UpdateAsync();
                logger.LogInformation("DNS update response: {response}", response);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "An error occurred while updating the DNS");
            }
            finally
            {
                try
                {
                    await Task.Delay(interval, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    logger.LogInformation("Task delay has been canceled");
                }
            }
        }
    }

    private static ServiceProvider Init()
    {
        var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        var services = new ServiceCollection();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt")
            .CreateLogger();

        services.AddLogging();
        services.AddSerilog();

        services.AddSingleton<IConfiguration>(configuration);
        services.Configure<DynamicDnsSettings>(configuration.GetSection(nameof(DynamicDnsSettings)));
        services.AddScoped<DynamicDnsClient>();

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
}