using Application.Commands;
using BuildingBlocks;
using MediatR;
using Tender;

namespace Services;

public class OpenTenderCommandCommandHandler(ITenderRepository tenderRepository) : IRequestHandler<OpenTenderCommand>
{
    public async Task Handle(OpenTenderCommand request, CancellationToken cancellationToken)
    {
        var tender = Tender.Tender.CreateInstance(
            request.Title,
            request.Description,
            new DateTimeFrame(request.StartTimeFrame, request.EndTimeFrame),
            new PricingFrame<long>(request.StartPricingFrame, request.EndPricingFrame)
        );

        await tenderRepository.OpenTender(tender);
    }
}