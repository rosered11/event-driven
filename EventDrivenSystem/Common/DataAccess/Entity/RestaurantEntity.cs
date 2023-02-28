namespace Rosered11.Common.DataAcess.Entity;

public class RestaurantEntity
{
    public Guid RestaurantId { get; set; }
    public Guid ProductId { get; set; }
    public string RestaurantName { get; set; }
    public bool RestaurantActive { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public bool ProductAvailable { get; set; }
}