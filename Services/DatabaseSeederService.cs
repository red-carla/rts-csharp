using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RTS.EntityFramework;

namespace RTS.Services;

public class DatabaseSeederService : IHostedService
{
    private readonly ILogger<DatabaseSeederService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public DatabaseSeederService(IServiceProvider serviceProvider, ILogger<DatabaseSeederService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<RecruitmentDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<DatabaseSeeder>>();

            var seeder = new DatabaseSeeder(dbContext, logger);
            seeder.Seed();
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}