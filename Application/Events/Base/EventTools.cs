using BuildingBlocks;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Exception;

namespace Application.Events.Base;

public static class EventTools
{
    private static readonly Assembly[] ApplicationAssembly = [typeof(IApplicationFlag).Assembly];

    private static readonly Dictionary<Type, Type[]> TypesMap = [];
    public static IEnumerable<INotification> ToApplicationEvent(this IAggregateEvent domainEvent)
    {
        var eventType = domainEvent.GetType();
        var baseType = typeof(IContractEventChain<>).MakeGenericType(eventType);

        var domainTypes = TypesMap.FirstOrDefault(x => x.Key == eventType).Value;

        if (domainTypes is null)
        {
            domainTypes = ApplicationAssembly
                .SelectMany(x => x.GetTypes())
                .Where(t => baseType.IsAssignableFrom(t) && t.IsClass)
                .ToArray() ?? [];

            if (!domainTypes!.Any()) throw new LogicException($"Application Event For Domain Event '{eventType.FullName}' Not Found");
            if (!domainTypes!.All(t => t.GetConstructor([]) is not null)) throw new LogicException($"Domain Events Must Have Default Constructor");
        }

        TypesMap[eventType] = domainTypes;

        return domainTypes!.Select(t =>
        {
            var result =(Activator.CreateInstance(t)! as INotification)!;

            t!.GetProperty("DomainEvent")!.SetValue(result,domainEvent);
            
            return result;
        })!.ToArray();
    }
}