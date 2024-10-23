using Application.Events;
using Application.Events.Base;
using BuildingBlocks;
using MediatR;
using SharedIdentity;
using Tender;

namespace Services.ApplicationEvent;

public class OpenTenderApplicationEventHandler : INotificationHandler<OpenTenderApplicationEvent>
{
    public Task Handle(OpenTenderApplicationEvent notification, CancellationToken cancellationToken)
    {
        var id = (notification.DomainEvent.Sender as IAggregate<TenderId>)!.Id!;
        Console.WriteLine($"Tender With Id {id} Now is Open");
        return Task.CompletedTask;
    }
}