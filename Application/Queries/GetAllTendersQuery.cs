using Application.DTOs;
using BuildingBlocks;
using MediatR;

namespace Application.Queries;

public class GetAllTendersQuery : BaseListQuery<Tender.Tender, GetAllTendersFilter>, IRequest<ListResult<TenderDto>>
{

}