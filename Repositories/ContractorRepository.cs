using BuildingBlocks;
using Contractor;
using Microsoft.EntityFrameworkCore;
using Persistence;
using SharedIdentity;

namespace Repositories;

public class ContractorRepository(PhoenixDbContext context) : IContractorRepository
{
    public async Task<ListResult<Contractor.Contractor>> GeAllAsync(BaseListQuery<Contractor.Contractor> filter)
    {
        var result = await context.Contractors.ToListResultAsync(filter, CancellationToken.None);
        return result;
    }

    public async Task<List<Contractor.Contractor>> GeAllAsync(IEnumerable<ContractorId> ids)
    {
        var result = await context.Contractors.Where(x=> ids.Contains(x.Id)).ToListAsync();
        return result;
    }

    public async Task<Contractor.Contractor?> GetByIdAsync(ContractorId id)
    {
        var result = await context.Contractors.FirstOrDefaultAsync(x => id == x.Id);
        return result;
    }

    public Task AddContractor(Contractor.Contractor contractor)
    {
        context.Add(contractor);
        return context.SaveChangesAsync();
    }
}