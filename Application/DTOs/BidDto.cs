using Tender;

namespace Application.DTOs;

public class BidDto
{
    public static BidDto CreateInstance(ContractorBid bid, Contractor.Contractor contractor)
    {
        return new BidDto()
        {
            ContractorId = contractor.Id,
            Winner = bid.Winner,
            BidAmount = bid.BidAmount,
            ContractorName = contractor.Name,
            CreateDate = bid.CreateDate,
        };
    }

    public long ContractorId { get; set; }
    public string ContractorName { get; set; } = null!;
    public long BidAmount { get; set; }
    public bool Winner { get; set; }
    public DateTime CreateDate { get; set; }
}