using Common.CommonDomain.Event;

namespace Rosered11.OrderService.Domain.Event
{
    public class OrderCancelledEvent : OrderEvent
    {
        public OrderCancelledEvent(Entities.Order? order, DateTimeOffset createdAt) : base(order, createdAt)
        {
        }
    }
}