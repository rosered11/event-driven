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
        private string _createdAt;
        private PaymentStatus _paymentStatus;
        private List<string> _failureMessage;
    }
}