using MediatR;

namespace Application.Commands;

public record BidToTenderCommand : IRequest
{
    public long ContractorId { get; set; }
    public long TenderId { get; set; }
    public long BidAmount { get; set; }
}