using Application.Commands;
using MediatR;
using SharedIdentity;
using Tender;

namespace Services;

public class CloseTenderCommandHandler(ITenderRepository tenderRepository) : IRequestHandler<CloseTenderCommand>
{
    public async Task Handle(CloseTenderCommand request, CancellationToken cancellationToken)
    {
        await tenderRepository.CloseTender(new TenderId(request.TenderId));
    }
}