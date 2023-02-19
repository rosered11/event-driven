using Rosered11.OrderService.Common.ValueObject;

namespace Rosered11.Kafka.Model
{
    public class RestaurantApprovalResponseModel
    {
        public string? Id { get; set; }
        public string? SagaId { get; set; }
        public string? RestaurantId { get; set; }
        public string? OrderId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public OrderApprovalStatus OrderApprovalStatus { get; set; }
        public List<string>? FailureMessages { get; set; }
    }
}