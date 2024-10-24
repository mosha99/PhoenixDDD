using SharedIdentity;

namespace Tender;

public sealed record ContractorBid(ContractorId ContractorId, long BidAmount)
{
    public bool Winner { get; init; } = false;
    public DateTime CreateDate { get; } = DateTime.Now;
}