using Application.DTOs;
using Application.Queries;
using BuildingBlocks;
using Contractor;
using MediatR;
using Tender;

namespace Services;

public class GetAllContractorQueryHandler(ITenderRepository tenderRepository, IContractorRepository contractorRepository) : IRequestHandler<GetAllContractorQuery, ListResult<ContractorDto>>
{
    public async Task<ListResult<ContractorDto>> Handle(GetAllContractorQuery request, CancellationToken cancellationToken)
    {
        var contractors = await contractorRepository.GeAllAsync(request);

        var tenders = await tenderRepository.GetByContractor(contractors.List.Select(x => x.Id));

        var tendersDto = tenders.Select(t => TenderDto.CreateInstance(t, contractors.List));

        return new ListResult<ContractorDto>()
        {
            Count = contractors.Count,
            List = contractors.List.Select(x => ContractorDto.CreateInstance(x, tendersDto)).ToList()
        };
    }
}