using BuildingBlocks;
using BuildingBlocks.Exception;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Persistence.Tools;

public class IdValueGenerator<TId> : ValueGenerator<TId> where TId : class, IIdentityCreator
{
    public IdValueGenerator() : base()
    {

    }

    public override async ValueTask<TId> NextAsync(EntityEntry entry, CancellationToken cancellationToken = new CancellationToken())
    {
        return await GenerateIdAsync(entry.Context, cancellationToken);
    }

    public override TId Next(EntityEntry entry)
    {
        return GenerateId(entry.Context);
    }

    public override bool GeneratesTemporaryValues { get; }

    private static async Task<TId> GenerateIdAsync(DbContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var ids = await CreateQuery(context).ToListAsync(cancellationToken);

        return TId.CreateInstance(ids.Single()) as TId ?? throw new LogicException();
    }
    private static TId GenerateId(DbContext context)
    {
        var ids = CreateQuery(context).ToList();

        return TId.CreateInstance(ids.Single()) as TId ?? throw new LogicException();
    }

    private static IQueryable<long> CreateQuery(DbContext context)
    {
        var sequenceName = IdentityBase.GetSequenceBase<TId>();

        string query = $"DECLARE @seq BIGINT = NEXT VALUE FOR [" + sequenceName + "]; select @seq;";

        return context.Database.SqlQueryRaw<long>(query);
    }
}