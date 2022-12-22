using Common.CommonDomain.Event;

namespace Rosered11.OrderService.Domain.Event
{
    public class OrderPaidEvent : OrderEvent
    {
        public OrderPaidEvent(Entities.Order? order, DateTimeOffset createdAt) : base(order, createdAt)
        {
        }
    }
}