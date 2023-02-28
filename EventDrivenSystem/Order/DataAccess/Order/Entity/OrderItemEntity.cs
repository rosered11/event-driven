namespace Rosered11.Order.DataAccess.Order.Entity;

public class OrderItemEntity
{
    public long Id { get; set; }
    public OrderEntity Order { get; set; }
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotal { get; set; }
}