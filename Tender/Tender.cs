using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using BuildingBlocks;
using BuildingBlocks.Exception;
using SharedIdentity;
using Tender.Exception;

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

        if (string.IsNullOrWhiteSpace(title)) throw new LogicException("Title is required");
        if (string.IsNullOrWhiteSpace(description)) throw new LogicException("Description is required");
        if (dateTimeFrame is null) throw new LogicException("DateTimeFrame is required");
        if (pricingFrame is null) throw new LogicException("PricingFrame is required");

        var tender = new Tender()
        {
            OpeDateTime = DateTime.Now,
            State = TenderState.Open,
            Title = title,
            Description = description,
            DateTimeFrame = dateTimeFrame,
            PricingFrame = pricingFrame,
        };

        tender.AddEvent(new OpenTenderEvent());
        //todo Rise Open Event

        return tender;
    }

    public string Title { get; private init; } = null!;
    public string Description { get; private init; } = null!;
    public DateTimeFrame DateTimeFrame { get; private init; } = null!;
    public PricingFrame<long> PricingFrame { get; private init; } = null!;
    public IReadOnlyCollection<ContractorBid> Bids { get; private set; } = new List<ContractorBid>();
    public TenderState State { get; private set; }
    public DateTime OpeDateTime { get; private init; }
    public DateTime? CloseDate { get; private set; }
    public DateTime? CancelDate { get; private set; }

    public ContractorId? Winner => Bids.SingleOrDefault(x => x.Winner)?.ContractorId;
    public void AddBid(ContractorBid bid)
    {
        if (State != TenderState.Open) throw new LogicException("Cannot add Bid to Closed Tender");

        if (bid.Winner == true) throw new LogicException("Cannot add Invalid Bid to Tender");

        if (DateTimeFrame.IsValidDate(DateTime.Now) != true) throw new LogicException($"We are not allowed in the time frame. Allowed ({DateTimeFrame.Start:G} to {DateTimeFrame.End:G})");

        if (PricingFrame.IsValidAmount(bid.BidAmount) != true) throw new LogicException($"Amount is not Allowed. Allowed ({PricingFrame.Start:N0} to {PricingFrame.End:N0})");
        var list = Bids.ToList();
        list.Add(bid);
        Bids = list.AsReadOnly();
    }
    public void Close()
    {
        if (!Bids.Any()) throw new TenderInvalidStateForCloseException();

        var winner = Bids.MinBy(x => x.BidAmount);

        winner = winner! with { Winner = true };

        State = TenderState.Closed;

        CloseDate = DateTime.Now;
        //todo Rise Win Event
    }
    public void Cancel()
    {
        if (State != TenderState.Open) throw new LogicException("You Can Cancel Only Open Tender");
        if (Bids.Any(x => x.Winner)) throw new LogicException("Cannot Cancel Tender With Winner");
        CancelDate = DateTime.Now;
        State = TenderState.Canceled;

        //todo Rise Cancel Event
    }
}