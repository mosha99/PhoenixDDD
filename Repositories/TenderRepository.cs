using BuildingBlocks;
using Microsoft.EntityFrameworkCore;
using Persistence;
using SharedIdentity;
using System.Reflection;
using BuildingBlocks.Exception;
using Tender;

namespace Repositories;

public class TenderRepository(PhoenixDbContext context , IAggregateEventProvider publisher) : ITenderRepository
{
    public async Task<ListResult<Tender.Tender>> GetAllAsync(BaseListQuery<Tender.Tender> filter)
    {
        var result = await context.Tenders.ToListResultAsync(filter, CancellationToken.None);
        return result;
    }

    public async Task<List<Tender.Tender>> GetByContractor(ContractorId contractorId)
    {
        var result =
            await context.Tenders
                .Where(t => t.Bids.Any(b => b.ContractorId == contractorId))
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();

        return result;
    }

    public async Task<List<Tender.Tender>> GetByContractor(IEnumerable<ContractorId> contractorId)
    {
        var result =
            await context.Tenders
                .Where(t => t.Bids.Any(b => contractorId.Contains(b.ContractorId)))
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync();

        return result;
    }

    public async Task<Tender.Tender?> GetByIdAsync(TenderId id)
    {
        var tender = await context.Tenders
            .Include(x => x.Bids)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(x => x.Id == id);

        return tender;
    }

    public async Task BidToTender(TenderId id, ContractorBid bid)
    {
        var tender = await context.Tenders.FirstOrDefaultAsync(x => x.Id == id);

        if (tender is null) throw new LogicException("Tender not found");

        tender.AddBid(bid);

        await context.SaveChangesAsync();

        await publisher.Invoke(tender.Events);
    }

    public async Task CloseTender(TenderId id)
    {
        var tender = await context.Tenders.FirstOrDefaultAsync(x => x.Id == id);

        if (tender is null) throw new LogicException("Tender not found");

        tender.Close();

        await context.SaveChangesAsync();

        await publisher.Invoke(tender.Events);
    }

    public async Task CancelTender(TenderId id)
    {
        var tender = await context.Tenders.FirstOrDefaultAsync(x => x.Id == id);

        if (tender is null) throw new LogicException("Tender not found");

        tender.Cancel();

        await context.SaveChangesAsync();

        await publisher.Invoke(tender.Events);
    }

    public async Task OpenTender(Tender.Tender tender)
    {
        context.Add(tender);

        await context.SaveChangesAsync();

        await publisher.Invoke(tender.Events);
    }
}