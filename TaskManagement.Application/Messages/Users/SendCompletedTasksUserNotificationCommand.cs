using MediatR;

namespace TaskManagement.Application.Services
{
    public class SendCompletedTasksUserNotificationCommand : IRequest
    {
        public string UserEmail { get; set; } = string.Empty;
    }
}