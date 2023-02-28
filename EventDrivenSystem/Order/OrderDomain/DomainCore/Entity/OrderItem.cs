using Rosered11.Common.Domain.Entity;
using Rosered11.Common.Domain.ValueObject;
using Rosered11.Order.Domain.Core.ValueObject;

namespace Rosered11.Order.Domain.Core.Entity
{
    public class OrderItem : BaseEntity<OrderItemId>
    {
        public OrderId? OrderId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public Money Price { get; set; } = Money.Zero;
        public Money SubTotal { get; set; } = Money.Zero;

        public OrderItem(OrderItemId id) : base(id){
        }
        public void initializeOrderItem(OrderId orderId, OrderItemId orderItemId){
            ID = orderItemId;
            OrderId = orderId;
        }

        public bool isPriceValid() {
            return  Product != null &&
                    Price.isGreaterThanZero() &&
                    Price.Equals(Product.Price) &&
                    Price.multiply(Quantity).Equals(SubTotal);
        }
    }
}