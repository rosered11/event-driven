using Rosered11.OrderService.Common.ValueObject;

namespace Rosered11.OrderService.Domain.DTO.Message
{
    public class RestaurantApprovalResponse
    {
        private string _id;
        private string _sagaId;
        private string _orderId;
        private string _restaurantId;
        private string _createdAt;
        private OrderApprovalStatus _orderApprovalStatus;
        private List<string> _failureMessage;
    }
}