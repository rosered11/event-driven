namespace Rosered11.Order.DataAccess.Order.Entity;

public class OrderAddressEntity
{
    public Guid Id { get; set; }
    public virtual OrderEntity Order { get; set; }

    public string Street { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
}