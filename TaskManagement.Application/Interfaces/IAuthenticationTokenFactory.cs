namespace TaskManagement.Application.Interfaces
{
    public interface IAuthenticationTokenFactory
    {
        string GenerateToken(string email);
    }
}