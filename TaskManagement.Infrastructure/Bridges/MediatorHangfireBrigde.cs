using MediatR;

namespace TaskManagement.Infrastructure.Bridges
{
    public class MediatorHangfireBrigde
    {
        private readonly IMediator _mediator;

        public MediatorHangfireBrigde(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Send(IRequest request)
        {
            await _mediator.Send(request);
        }
    }
}
