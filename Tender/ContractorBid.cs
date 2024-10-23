using SharedIdentity;

namespace Tender;

public sealed class ContractorBid(ContractorId contractorId, long bidAmount)
{
    public ContractorId ContractorId { get; private set; } = contractorId;
    public long BidAmount { get; private set; } = bidAmount;
    public bool Winner { get; private set; } = false;
    public DateTime CreateDate { get; private set; } = DateTime.Now;
    public void Win()
        => Winner = true;
}