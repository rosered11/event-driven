using Rosered11.Common.Domain.Entity;

namespace Rosered11.Order.Domain.Core.Event
{
    public class OrderCancelledEvent : OrderEvent
    {
        public OrderCancelledEvent(Entity.Order order, ZoneDateTime createdAt) : base(order, createdAt)
        {
        }
    }
}