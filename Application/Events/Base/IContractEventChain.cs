using BuildingBlocks;
using MediatR;

namespace Application.Events.Base;

public interface IContractEventChain<T> : INotification where T : class, IAggregateEvent
{
    public T DomainEvent { get; set; }
}