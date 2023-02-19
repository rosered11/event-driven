using Rosered11.OrderService.Common.ValueObject;

namespace Rosered11.OrderService.Domain.DTO.Message
{
    public class PaymentResponse
    {
        private string _id;
        private string _sagaId;
        private string _orderId;
        private string _paymentId;
        private string _customerId;
        private decimal _price;
        private DateTimeOffset _createdAt;
        private PaymentStatus _paymentStatus;
        private List<string> _failureMessage;

        public string Id { get => _id; set => _id = value; }
        public string SagaId { get => _sagaId; set => _sagaId = value; }
        public string OrderId { get => _orderId; set => _orderId = value; }
        public string PaymentId { get => _paymentId; set => _paymentId = value; }
        public string CustomerId { get => _customerId; set => _customerId = value; }
        public decimal Price { get => _price; set => _price = value; }
        public DateTimeOffset CreatedAt { get => _createdAt; set => _createdAt = value; }
        public PaymentStatus PaymentStatus { get => _paymentStatus; set => _paymentStatus = value; }
        public List<string> FailureMessage { get => _failureMessage; set => _failureMessage = value; }
    }
}