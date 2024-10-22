using Application.Commands;
using Contractor;
using MediatR;
using SharedIdentity;
using Tender;

namespace Services;

public class BidToTenderCommandHandler(ITenderRepository tenderRepository, IContractorRepository contractorRepository) : IRequestHandler<BidToTenderCommand>
{
    public async Task Handle(BidToTenderCommand request, CancellationToken cancellationToken)
    {
        var contractor = (await contractorRepository.GetByIdAsync(new ContractorId(request.ContractorId)))
                         ?? throw new ArgumentException("Contractor Not Find");

        var tender = (await tenderRepository.GetByIdAsync(new TenderId(request.TenderId)))
                     ?? throw new ArgumentException("Tender Not Find");

        var bid = new ContractorBid(contractor.Id, request.BidAmount);

        await tenderRepository.BidToTender(tender.Id, bid);
    }
}