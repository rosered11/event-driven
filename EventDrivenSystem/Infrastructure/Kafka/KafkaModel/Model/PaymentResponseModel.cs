using Rosered11.OrderService.Common.ValueObject;

namespace Rosered11.Kafka.Model
{
    public class PaymentResponseModel
    {
        public string? Id { get; set; }
        public string? SagaId { get; set; }
        public string? PaymentId { get; set; }
        public string? CustomerId { get; set; }
        public string? OrderId { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public List<string>? FailureMessages { get; set; }
    }
}