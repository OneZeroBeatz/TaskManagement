using MediatR;
using TaskManagement.Shared;

namespace TaskManagement.Application.Messages.Users;

public record LoginCommand : IRequest<Result<string>>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
