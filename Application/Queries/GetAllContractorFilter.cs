using Application.DTOs;
using BuildingBlocks;
using MediatR;

namespace Application.Queries;

public class GetAllContractorFilter() : BaseFilter<Contractor.Contractor>, IRequest<ListResult<ContractorDto>>
{
    public string? Name { get; set; }

    public override void InitExpression()
    {
        AddExpression(Name,contractor => contractor.Name.Contains(Name!));
    }
}