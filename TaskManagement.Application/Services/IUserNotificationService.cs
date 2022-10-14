namespace TaskManagement.Application.Services
{
    public interface IUserNotificationService
    {
        Task NotifyUser(string userEmail);
    }
}