using Rosered11.OrderService.Domain.DTO.Message;

namespace Rosered11.OrderService.Domain.Ports.Input.Message.Listener.RestaurantApproval
{
    public interface RestaurantApprovalResponseMessageListener
    {
        void OrderApproved(RestaurantApprovalResponse restaurantApprovalResponse);
        void OrderRejected(RestaurantApprovalResponse restaurantApprovalResponse);
    }
}