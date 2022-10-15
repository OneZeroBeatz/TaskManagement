using MediatR;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Repositories;

namespace TaskManagement.Application.Services
{
    public class SendCompletedTasksUserNotificationCommandHandler : IRequestHandler<SendCompletedTasksUserNotificationCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IEmailSender _emailSender;

        public SendCompletedTasksUserNotificationCommandHandler(IUserRepository userRepository,
                                                                ITaskRepository taskRepository,
                                                                IEmailSender emailSender)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        public async Task<Unit> Handle(SendCompletedTasksUserNotificationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.UserEmail, cancellationToken);

            if (user == null)
                return Unit.Value;

            //TODO: Move to factory
            var userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(user.TimezoneId);

            var userZoneDatetimeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, userTimeZone);

            var previousDayUtcDate = userZoneDatetimeNow.AddDays(-1).ToUniversalTime().Date;

            var finishedTasksForDateCount = await _taskRepository.GetFinishedTasksForDateCountAsync(user.Id, previousDayUtcDate);

            var mailRequest = new MailRequest()
            {
                Subject = "Finished tasks",
                Body = $"You finished {finishedTasksForDateCount} tasks for last day",
                ToEmail = request.UserEmail
            };

            await _emailSender.SendEmailAsync(mailRequest);

            return Unit.Value;
        }
    }
}