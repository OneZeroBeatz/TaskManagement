namespace TaskManagement.Application.Interfaces
{
    public interface IAuthenticationTokenFactory
    {
        string GenerateToken(int userId, string email);
    }
}