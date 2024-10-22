using Application.Commands;
using MediatR;
using SharedIdentity;
using Tender;

namespace Services;

public class CancelTenderCommandHandler(ITenderRepository tenderRepository) : IRequestHandler<CancelTenderCommand>
{
    public async Task Handle(CancelTenderCommand request, CancellationToken cancellationToken)
    {
        await tenderRepository.CancelTender(new TenderId(request.TenderId));
    }
}