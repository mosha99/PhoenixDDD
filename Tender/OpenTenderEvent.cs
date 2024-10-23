using BuildingBlocks;

namespace Tender;

public class OpenTenderEvent() : IAggregateEvent
{
    public object Sender { get; set; } = null!;
}