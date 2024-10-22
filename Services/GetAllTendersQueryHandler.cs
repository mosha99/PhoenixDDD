using Application.DTOs;
using Application.Queries;
using BuildingBlocks;
using Contractor;
using MediatR;
using Tender;

namespace Services;

public class GetAllTendersQueryHandler(ITenderRepository tenderRepository, IContractorRepository contractorRepository) : IRequestHandler<GetAllTendersQuery, ListResult<TenderDto>>
{
    public async Task<ListResult<TenderDto>> Handle(GetAllTendersQuery request, CancellationToken cancellationToken)
    {
        var tenders = await tenderRepository.GetAllAsync(request);
        var contractors = await contractorRepository.GeAllAsync(tenders.List.SelectMany(x => x.Bids).Select(x => x.ContractorId));

        return new ListResult<TenderDto>()
        {
            Count = tenders.Count,
            List = MergeContractorsAndTenders(tenders.List, contractors)
        };
    }

    public static List<TenderDto> MergeContractorsAndTenders(List<Tender.Tender> tenders, List<Contractor.Contractor> contractors)
    {
        return tenders.Select(x => TenderDto.CreateInstance(x, contractors)).ToList();
    }
}