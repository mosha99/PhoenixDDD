using SharedIdentity;

namespace Tender;

public sealed class ContractorBid(ContractorId contractorId, long bidAmount)
{
    public ContractorId ContractorId { get; } = contractorId;
    public long BidAmount { get; private set; } = bidAmount;
    public bool Winner { get; private set; } = false;
    public DateTime CreateDate { get; private set; } = DateTime.Now;
    public ContractorBid? PreviousBid { get; private set; }

    public ContractorBid Modify(ContractorBid amount)
    {
        var newBid = new ContractorBid(amount.ContractorId, amount.BidAmount);
        newBid.PreviousBid = this;
        return newBid;
    }
    public void Win()
        => Winner = true;
}