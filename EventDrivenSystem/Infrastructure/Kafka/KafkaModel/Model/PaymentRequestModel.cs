using Rosered11.OrderService.Common.ValueObject;

namespace Rosered11.Kafka.Model
{
    public class PaymentRequestModel
    {
        public string? Id { get; set; }
        public string? SagaId { get; set; }
        public string? CustomerId { get; set; }
        public string? OrderId { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public PaymentOrderStatus PaymentOrderStatus { get; set; }
    }
}