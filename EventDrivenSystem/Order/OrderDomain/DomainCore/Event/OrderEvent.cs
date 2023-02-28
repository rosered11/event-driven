using Rosered11.Common.Domain.Entity;
using Rosered11.Common.Domain.Event;

namespace Rosered11.Order.Domain.Core.Event
{
    public abstract class OrderEvent : IDomainEvent<Entity.Order>
    {
        public Entity.Order Order { get; private set; }
        public ZoneDateTime CreatedAt { get; private set; }

        public OrderEvent(Entity.Order order, ZoneDateTime createdAt) {
            Order = order;
            CreatedAt = createdAt;
        }
    }
}