using TaskManagement.Application.Repositories;
using TaskManagement.Infrastructure.Services;

namespace TaskManagement.Api;

public class HostedService : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public HostedService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        var users = await userRepository.GetAll();

        await Task.Run(() =>
        {
            Parallel.ForEach(users, user =>
            {
                var timezone = TimeZoneInfo.FindSystemTimeZoneById(user.TimezoneId);
                notificationService.AddOrUpdateNotificationTimezone(user.Email, timezone);
            });

        }, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
