namespace BuildingBlocks;

public interface IAggregateEvent
{
    public object Sender { get; set; }
};
