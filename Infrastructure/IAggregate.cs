namespace BuildingBlocks;

public interface IAggregate<out TId> : IEntity
    where TId : IdentityBase
{
    TId Id { get; }
}