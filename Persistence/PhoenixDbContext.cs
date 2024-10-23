using BuildingBlocks;
using Microsoft.EntityFrameworkCore;
using Persistence.Tools;
using SharedIdentity;
using Tender;

namespace Persistence;

public class PhoenixDbContext : DbContext
{
    public PhoenixDbContext() { }

    private bool _created = false;

    public PhoenixDbContext(DbContextOptions<PhoenixDbContext> contextOptions) : base(contextOptions)
    {
        if (!_created)
        {
            _created = Database.EnsureCreated();
        }
    }

    public DbSet<Tender.Tender> Tenders { get; set; }
    public DbSet<Contractor.Contractor> Contractors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Owned<ContractorBid>();
        modelBuilder.Entity<Tender.Tender>(o =>
        {
            o.OwnsMany<ContractorBid>(x => x.Bids, b =>
            {
                b.ToJson();
                b.Property(x => x.ContractorId)
                    .HasConversion<TypedIdValueConverter<ContractorId>, TypedIdValueComparer<ContractorId>>()
                    .HasValueGenerator<IdValueGenerator<ContractorId>>();
            });
        });

        modelBuilder.Owned(typeof(DateTimeFrame));
        modelBuilder.Owned(typeof(PricingFrame<long>));

        ConfigureEntities(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    #region SaveChange

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        BeforeSaving();
        return base.SaveChangesAsync(cancellationToken);
    }
    public override int SaveChanges()
    {
        BeforeSaving();
        return base.SaveChanges();
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
    {
        BeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        BeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    void BeforeSaving()
    {
        var changedEntity = ChangeTracker.Entries<IEntity>();
        foreach (var entityEntry in changedEntity)
        {
            switch (entityEntry.State)
            {
                case EntityState.Added:
                    entityEntry.Entity.Version = 1;
                    entityEntry.Entity.CreatedAt = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entityEntry.Entity.Version += 1;
                    entityEntry.Entity.LastModified = DateTime.Now;
                    break;
                case EntityState.Deleted:
                    entityEntry.State = EntityState.Modified;
                    entityEntry.Entity.IsDeleted = true;
                    entityEntry.Entity.Version += 1;
                    entityEntry.Entity.LastModified = DateTime.Now;
                    break;
            }


        }
    }

    #endregion

    #region Configure Entities

    private void ConfigureEntities(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var type = entityType.ClrType;

            if (!typeof(IAggregate<IdentityBase>).IsAssignableFrom(type)) continue;

            var typeId = type.GetProperty(nameof(IAggregate<IdentityBase>.Id));

            typeof(PhoenixDbContext).CallMethod(nameof(InitKey), [type, typeId!.PropertyType], modelBuilder);
        }
    }
    public static void InitKey<TEntity, TId>(ModelBuilder modelBuilder)
        where TId : IdentityBase, IIdentityCreator
        where TEntity : AggregateBase<TId>
    {
        string sequenceName = IdentityBase.GetSequenceBase<TId>();

        modelBuilder.HasSequence<long>(sequenceName).StartsAt(1000).IncrementsBy(1);
        modelBuilder
            .Entity<TEntity>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Id)
                    .HasConversion<TypedIdValueConverter<TId>, TypedIdValueComparer<TId>>()
                    .HasValueGenerator<IdValueGenerator<TId>>();
            });
    }

    #endregion
}