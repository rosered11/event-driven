using Rosered11.OrderService.Common.ValueObject;

namespace Rosered11.Kafka.Model
{
    public class RestaurantApprovalRequestModel
    {
        public string? Id { get; set; }
        public string? SagaId { get; set; }
        public string? RestaurantId { get; set; }
        public string? OrderId { get; set; }
        public RestaurantOrderStatus RestaurantOrderStatus { get; set; }
        public List<Product>? Products { get; set; }
        public decimal Price { get; set; }
        //Wait implement
        public DateTimeOffset CreatedAt { get; set; }
        public record Product(string? Id, int Quantity);
    }
}