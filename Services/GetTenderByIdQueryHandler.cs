using Application.DTOs;
using Application.Queries;
using Contractor;
using MediatR;
using SharedIdentity;
using Tender;

namespace Services;

public class GetTenderByIdQueryHandler(ITenderRepository tenderRepository, IContractorRepository contractorRepository) : IRequestHandler<GetTenderQuery, TenderDto>
{
    public async Task<TenderDto> Handle(GetTenderQuery request, CancellationToken cancellationToken)
    {
        var tender = await tenderRepository.GetByIdAsync(new TenderId(request.TenderId));
        var contractors = await contractorRepository.GeAllAsync(tender.Bids.Select(x => x.ContractorId));

        return TenderDto.CreateInstance(tender, contractors);
    }
}