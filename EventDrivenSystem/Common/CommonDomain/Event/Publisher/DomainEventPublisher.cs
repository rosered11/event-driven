using Common.CommonDomain.Event;

namespace Rosered11.OrderService.Common.Event.Publisher
{
    public interface DomainEventPublisher<T, TEvent>
        where T : DomainEvent<TEvent>
    {
        void Publish(T domainEvent);
    }
}