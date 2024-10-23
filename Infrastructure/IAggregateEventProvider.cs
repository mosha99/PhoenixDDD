namespace BuildingBlocks;

public interface IAggregateEventProvider
{
    Task Invoke(IEnumerable<IAggregateEvent> events);
}