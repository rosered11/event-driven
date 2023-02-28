
using Rosered11.Common.Domain.Entity;

namespace Rosered11.Order.Domain.Core.Event
{
    public class OrderCreatedEvent : OrderEvent
    {
        public OrderCreatedEvent(Entity.Order order, ZoneDateTime createdAt) : base(order, createdAt)
        {
        }
    }
}