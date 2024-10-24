using Application.Events.Base;
using BuildingBlocks;
using MediatR;

namespace Services.ApplicationEvent.Tool;

public class MediatrAggregateEventProvider(IPublisher publisher) : IAggregateEventProvider
{
    public async Task Invoke(IEnumerable<IAggregateEvent> events)
    {
        var applicationEvents = events.Select(x => x.ToApplicationEvent()).ToList();
        foreach (var e in applicationEvents.SelectMany(x=>x))
        {
            await publisher.Publish(e);
        }
    }
}