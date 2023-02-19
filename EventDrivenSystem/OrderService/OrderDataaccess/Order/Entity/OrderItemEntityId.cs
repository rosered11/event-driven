namespace Rosered11.OrderService.DataAccess.Entity
{
    public class OrderItemEntityId
    {
        public long Id { get; set; }
        public OrderEntity? Order { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                OrderItemEntityId that = (OrderItemEntityId) obj;
                return Id.Equals(that.Id) && Order == that.Order;
            }
        }
        
        public override int GetHashCode()
        {
            return Tuple.Create(Id, Order).GetHashCode();
        }

    }
}