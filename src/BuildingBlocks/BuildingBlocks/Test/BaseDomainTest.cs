using BuildingBlocks.Core.Event;
using BuildingBlocks.Core.Model;

namespace BuildingBlocks.Test;

public abstract class BaseDomainTest
{
    protected static T AssertDomainEventWasPublished<T>(IAggregate entity) where T : IDomainEvent
    {
        var domainEvent = entity.DomainEvents.OfType<T>().SingleOrDefault();

        if (domainEvent == null)
            throw new Exception($"{typeof(T).Name} was not published.");

        return domainEvent;
    }
}