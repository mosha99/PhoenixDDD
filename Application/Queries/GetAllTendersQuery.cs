using Application.DTOs;
using BuildingBlocks;
using MediatR;

namespace Application.Queries;

public class GetAllTendersQuery : BaseFilter<Tender.Tender>, IRequest<ListResult<TenderDto>>
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public List<long>? ContractorIds { get; set; }

    public override void InitExpression()
    {
        AddExpression(Title, t => t.Title.Contains(Title!));
        AddExpression(Description, t => t.Title.Contains(Description!));
        if (ContractorIds?.Any() == true)
            AddExpression(ContractorIds,
                t => t.Bids.Any(b => ContractorIds.Contains(b.ContractorId)));
    }
}