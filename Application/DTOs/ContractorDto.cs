namespace Application.DTOs;

public class ContractorDto
{
    public static ContractorDto CreateInstance(Contractor.Contractor contractor , IEnumerable<TenderDto> tenders)
    {
        return new ContractorDto()
        {
            Id = contractor.Id,
            Name = contractor.Name,
            PhoneNumber = contractor.PhoneNumber,
            Tenders = tenders
        };
    }
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public IEnumerable<TenderDto> Tenders { get; set; }
    public IEnumerable<TenderDto> WinedTenders => Tenders.Where(x => x.Winner?.ContractorId == Id);
}