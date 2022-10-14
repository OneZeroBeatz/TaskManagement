using MediatR;
using TaskManagement.Shared;

namespace TaskManagement.Application.Messages.Tasks;

public record DeleteTaskCommand : IRequest<Result>
{
    public int TaskId { get; set; }
    public int UserId { get; set; }
}