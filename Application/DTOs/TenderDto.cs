using SharedIdentity;
using Tender;

namespace Application.DTOs;

public class TenderDto
{
    public static TenderDto CreateInstance(Tender.Tender tender, IEnumerable<Contractor.Contractor> contractors)
    {
        return new TenderDto()
        {
            Title = tender.Title,
            Description = tender.Description,
            StartTimeFrame = tender.DateTimeFrame.Start,
            EndTimeFrame = tender.DateTimeFrame.End,
            StartPricingFrame = tender.PricingFrame.Start,
            EndPricingFrame = tender.PricingFrame.End,
            Bids = tender.Bids
                .Where(x=> contractors.Any(b => b.Id == x.ContractorId))
                .Select(x => BidDto.CreateInstance(x, contractors.First(c => c.Id == x.ContractorId))),
            State = tender.State,
            OpeDateTime = tender.OpeDateTime,
            CloseDate = tender.CloseDate,
            CancelDate = tender.CancelDate
        };
    }

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartTimeFrame { get; set; }
    public DateTime EndTimeFrame { get; set; }
    public long StartPricingFrame { get; set; }
    public long EndPricingFrame { get; set; }
    public IEnumerable<BidDto> Bids { get; set; } = [];
    public TenderState State { get; set; }
    public DateTime OpeDateTime { get; init; }
    public DateTime? CloseDate { get; set; }
    public DateTime? CancelDate { get; set; }
    public BidDto? HighestBid => Bids.MaxBy(x => x!.BidAmount);
    public BidDto? LowestBid => Bids.MinBy(x => x!.BidAmount);
    public BidDto? Winner => Bids.SingleOrDefault(x => x!.Winner);
}