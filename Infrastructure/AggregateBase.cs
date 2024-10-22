namespace BuildingBlocks;
public abstract class AggregateBase<TId>  : Entity, IAggregate<TId> where TId : IdentityBase, IIdentityCreator
{
    private readonly List<IAggregateEvent> _events = [];
    protected void AddEvent(IAggregateEvent aggregateEvent)
        => _events.Add(aggregateEvent);
    public IReadOnlyList<IAggregateEvent> Events => _events.AsReadOnly();
    public TId Id { get; private set; } = null!;
}