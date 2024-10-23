using Application.Events.Base;
using Tender;

namespace Application.Events;

public class OpenTenderApplicationEvent : IContractEventChain<OpenTenderEvent>
{
    public OpenTenderEvent DomainEvent { get; set; } = null!;
}