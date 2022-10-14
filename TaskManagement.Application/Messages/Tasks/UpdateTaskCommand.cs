using MediatR;
using TaskManagement.Shared;

namespace TaskManagement.Application.Messages.Tasks;

public record UpdateTaskCommand : IRequest<Result>
{
    public int TaskId { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
}