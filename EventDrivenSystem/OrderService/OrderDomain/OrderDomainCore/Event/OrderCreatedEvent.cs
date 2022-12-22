using Common.CommonDomain.Event;

namespace Rosered11.OrderService.Domain.Event
{
    public class OrderCreatedEvent : OrderEvent
    {
        public OrderCreatedEvent(Entities.Order? order, DateTimeOffset createdAt) : base(order, createdAt)
        {
        }
    }
}