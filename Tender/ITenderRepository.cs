using BuildingBlocks;
using SharedIdentity;

namespace Tender;

public interface ITenderRepository
{
    Task<ListResult<Tender>> GetAllAsync(BaseListQuery<Tender> filter);
    Task<List<Tender>> GetByContractor(ContractorId contractorId);
    Task<List<Tender>> GetByContractor(IEnumerable<ContractorId> contractorId);
    Task<Tender?> GetByIdAsync(TenderId id);
    Task BidToTender(TenderId id , ContractorBid bid);
    Task CloseTender(TenderId id);
    Task CancelTender(TenderId id);
    Task OpenTender(Tender tender);
}