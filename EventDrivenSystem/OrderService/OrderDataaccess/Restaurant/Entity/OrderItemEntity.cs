using System.ComponentModel.DataAnnotations.Schema;

namespace Rosered11.OrderService.DataAccess.Entity
{
    public class OrderItemEntity
    {
        public long Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }

        [ForeignKey("Order")]
        public Guid? OrderId { get; set; }
        public OrderEntity? Order { get; set; }

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