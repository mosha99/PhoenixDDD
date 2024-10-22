using System.Diagnostics.CodeAnalysis;
using BuildingBlocks;
using SharedIdentity;

namespace Tender;

public class Tender : AggregateBase<TenderId>
{
    private Tender() { }

    public static Tender CreateInstance(
        string title,
        string description,
        [DisallowNull] DateTimeFrame dateTimeFrame,
        [DisallowNull] PricingFrame<long> pricingFrame)
    {

        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required");
        if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Description is required");
        if (dateTimeFrame is null) throw new ArgumentException("DateTimeFrame is required");
        if (pricingFrame is null) throw new ArgumentException("PricingFrame is required");

        var tender = new Tender()
        {
            OpeDateTime = DateTime.Now,
            State = TenderState.Open,
            Title = title,
            Description = description,
            DateTimeFrame = dateTimeFrame,
            PricingFrame = pricingFrame,
        };

        //todo Rise Open Event

        return tender;
    }

    public string Title { get; private init; } = null!;
    public string Description { get; private init; } = null!;
    public DateTimeFrame DateTimeFrame { get; private init; } = null!;
    public PricingFrame<long> PricingFrame { get; private init; } = null!;
    public IReadOnlyList<ContractorBid> Bids { get; private set; } = [];
    public TenderState State { get; private set; }
    public DateTime OpeDateTime { get; private init; }
    public DateTime? CloseDate { get; private set; }
    public DateTime? CancelDate { get; private set; }

    public ContractorId? Winner => Bids.SingleOrDefault(x => x.Winner)?.ContractorId;
    public void AddBid(ContractorBid bid)
    {
        if (State != TenderState.Open) throw new Exception("Cannot add Bid to Closed Tender");

        if (bid.Winner == true) throw new Exception("Cannot add Invalid Bid to Tender");

        if (DateTimeFrame.IsValidDate(DateTime.Now) != true) throw new Exception($"We are not allowed in the time frame. Allowed ({DateTimeFrame.Start:G} to {DateTimeFrame.End:G})");

        if (PricingFrame.IsValidAmount(bid.BidAmount) != true) throw new Exception($"Amount is not Allowed. Allowed ({PricingFrame.Start:N0} to {PricingFrame.End:N0})");

        var bids = Bids.ToList();

        var finalBid = bids.FirstOrDefault(x => x.ContractorId == bid.ContractorId);

        if (finalBid is not null)
        {
            bid = finalBid.Modify(bid);
            bids.Remove(finalBid);
        }

        bids.Add(bid);

        Bids = bids.AsReadOnly();
    }
    public void SetWinnerAndClose(ContractorId winner)
    {
        if (State != TenderState.Open) throw new Exception("You Can Close Only Open Tender");

        var winnerBid = Bids.Single(x => x.ContractorId == winner);

        winnerBid.Win();

        Close();
    }
    public void SetWinnerAndClose()
    {
        var contractorBid = Bids.OrderByDescending(x => x.PreviousBid).First();
        SetWinnerAndClose(contractorBid.ContractorId);
    }
    private void Close()
    {
        var winner = Bids.Single(x => x.Winner);
        State = TenderState.Closed;
        CloseDate = DateTime.Now;
        //todo Rise Win Event
    }
    private void Cancel()
    {
        if (State != TenderState.Open) throw new Exception("You Can Cancel Only Open Tender");
        if (Bids.Any(x => x.Winner)) throw new Exception("Cannot Cancel Tender With Winner");
        CancelDate = DateTime.Now;
        State = TenderState.Canceled;

        //todo Rise Cancel Event
    }
}