using BuildingBlocks;
using SharedIdentity;

namespace Contractor;

public interface IContractorRepository
{
    Task<ListResult<Contractor>> GeAllAsync(BaseFilter<Contractor> filter);
    Task<List<Contractor>> GeAllAsync(IEnumerable<ContractorId> filter);
    Task<Contractor> GetByIdAsync(ContractorId id);
    Task AddContractor(Contractor contractor);
}