namespace BuildingBlocks;

public interface IAggregateEvent
{
    Task Invoke();
}