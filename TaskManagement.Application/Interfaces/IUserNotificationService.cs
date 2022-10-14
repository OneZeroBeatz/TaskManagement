namespace TaskManagement.Application.Interfaces
{
    public interface IUserNotificationService
    {
        Task NotifyUser(string userEmail);
    }
}