using Common.CommonDomain.Event;

namespace Rosered11.OrderService.Domain.Event
{
    public abstract class OrderEvent : DomainEvent<Entities.Order>
    {
        private readonly Entities.Order? _order;
        private readonly DateTimeOffset _createdAt;
        public OrderEvent(Entities.Order? order, DateTimeOffset createdAt)
        {
            _order = order;
            _createdAt = createdAt;
        }

        public Entities.Order? Order => _order;

        public DateTimeOffset CreatedAt => _createdAt;
    }
}