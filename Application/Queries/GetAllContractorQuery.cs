using Application.DTOs;
using BuildingBlocks;
using MediatR;

namespace Application.Queries;

public class GetAllContractorQuery() : BaseFilter<Contractor.Contractor>, IRequest<ListResult<ContractorDto>>
{
    public override void InitExpression() { }
}