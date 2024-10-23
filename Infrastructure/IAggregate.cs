namespace BuildingBlocks;

public interface IAggregate<out TId> : IEntity
    where TId : IdentityBase
{
    IReadOnlyList<IAggregateEvent> Events { get; }
    TId Id { get; }
}