using TaskManagement.Application.Repositories;
using TaskManagement.Infrastructure.Services;

namespace TaskManagement.Api
{
    public class HostedService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<HostedService> _logger;

        public HostedService(IServiceScopeFactory serviceScopeFactory, ILogger<HostedService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating notification job for each user started.");
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                var users = await userRepository.GetAll();

                await Task.Run(() =>
                {
                    Parallel.ForEach(users, user =>
                    {
                        Thread.Sleep(1000);
                        var timezone = TimeZoneInfo.FindSystemTimeZoneById(user.TimezoneId);
                        notificationService.AddOrUpdateNotificationTimezone(user.Email, timezone);
                    });

                }, cancellationToken);

                _logger.LogInformation("Creating notification job for each user finished.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Creating notification job for each user failed.");
                throw;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
