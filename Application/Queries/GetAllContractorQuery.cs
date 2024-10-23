using Application.DTOs;
using BuildingBlocks;
using MediatR;

namespace Application.Queries;

public class GetAllContractorQuery() : BaseListQuery<Contractor.Contractor,GetAllContractorFilter>, IRequest<ListResult<ContractorDto>>
{
}