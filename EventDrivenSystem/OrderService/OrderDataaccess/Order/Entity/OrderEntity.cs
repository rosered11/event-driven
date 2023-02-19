using System.ComponentModel.DataAnnotations.Schema;
using Common.CommonDomain.ValueObject;
using Rosered11.OrderService.Common.DataAccess;

namespace Rosered11.OrderService.DataAccess.Entity
{
    public class OrderEntity : BaseEntity<Guid>
    {
        public OrderEntity(Guid id, List<OrderItemEntity> items, OrderAddressEntity address) : base(id)
        {
            Items = items;
            Address = address;
        }

        public Guid CustomerId { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid TrackingId { get; set; }
        public decimal Price { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string? FailureMessage { get; set; }

        [InverseProperty("Order")]
        public OrderAddressEntity Address { get; set; }
        public List<OrderItemEntity> Items { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                OrderEntity that = (OrderEntity) obj;
                return Id.Equals(that.Id);
            }
        }
        
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}