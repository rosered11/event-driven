namespace Rosered11.Order.DataAccess.Order.Entity;

public class OrderEntity
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public Guid RestaurantId { get; set; }
    public Guid TrackingId { get; set; }
    public decimal Price { get; set; }
    public string OrderStatus { get; set; }
    public string FailureMessages { get; set; }

    public virtual OrderAddressEntity Address { get; set; }
    public virtual List<OrderItemEntity> Items { get; set; }
}