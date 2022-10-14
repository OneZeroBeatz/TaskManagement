using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Services
{
    public class UserNotificationService : IUserNotificationService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IEmailSender _emailSender;

        public UserNotificationService(IUserRepository userRepository, ITaskRepository taskRepository, IEmailSender emailSender)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        public async Task NotifyUser(string userEmail)
        {
            var user = await _userRepository.GetByEmailAsync(userEmail, CancellationToken.None);

            if (user == null)
                return;

            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(user.TimezoneId);

            var userZoneDatetimeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, userTimeZone);

            var previousDayUtcDate = userZoneDatetimeNow.AddDays(-1).ToUniversalTime().Date;

            var finishedTasksForDateCount = await _taskRepository.GetFinishedTasksForDateCountAsync(user.Id, previousDayUtcDate);

            var mailRequest = new MailRequest()
            {
                Subject = "Finished tasks",
                Body = $"You finished {finishedTasksForDateCount} tasks for last day",
                ToEmail = userEmail
            };

            await _emailSender.SendEmailAsync(mailRequest);
        }
    }
}