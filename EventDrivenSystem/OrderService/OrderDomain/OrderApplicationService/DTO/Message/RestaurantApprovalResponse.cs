using Rosered11.OrderService.Common.ValueObject;

namespace Rosered11.OrderService.Domain.DTO.Message
{
    public class RestaurantApprovalResponse
    {
        private string _id;
        private string _sagaId;
        private string _orderId;
        private string _restaurantId;
        private DateTimeOffset? _createdAt;
        private OrderApprovalStatus _orderApprovalStatus;
        private List<string> _failureMessage;

        public string Id { get => _id; set => _id = value; }
        public string SagaId { get => _sagaId; set => _sagaId = value; }
        public string OrderId { get => _orderId; set => _orderId = value; }
        public string RestaurantId { get => _restaurantId; set => _restaurantId = value; }
        public DateTimeOffset? CreatedAt { get => _createdAt; set => _createdAt = value; }
        public OrderApprovalStatus OrderApprovalStatus { get => _orderApprovalStatus; set => _orderApprovalStatus = value; }
        public List<string> FailureMessage { get => _failureMessage; set => _failureMessage = value; }
    }
}