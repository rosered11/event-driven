using System.ComponentModel.DataAnnotations.Schema;
using Common.CommonDomain.ValueObject;

namespace Rosered11.OrderService.DataAccess.Entity
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid TrackingId { get; set; }
        public decimal Price { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string? FailureMessage { get; set; }

        [InverseProperty("Order")]
        public required OrderAddressEntity Address { get; set; }
        public required List<OrderItemEntity> Items { get; set; }

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