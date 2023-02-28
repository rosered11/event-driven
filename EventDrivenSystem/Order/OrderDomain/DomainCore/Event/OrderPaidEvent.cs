using Rosered11.Common.Domain.Entity;

namespace Rosered11.Order.Domain.Core.Event
{
    public class OrderPaidEvent : OrderEvent
    {
        public OrderPaidEvent(Entity.Order order, ZoneDateTime createdAt) : base(order, createdAt)
        {
        }
    }
}