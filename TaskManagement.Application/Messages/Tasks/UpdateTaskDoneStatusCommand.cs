using MediatR;
using TaskManagement.Shared;

namespace TaskManagement.Application.Messages.Tasks;

public record UpdateTaskDoneStatusCommand : IRequest<Result>
{
    public int TaskId { get; set; }
    public int UserId { get; set; }
    public bool Done { get; set; }
}