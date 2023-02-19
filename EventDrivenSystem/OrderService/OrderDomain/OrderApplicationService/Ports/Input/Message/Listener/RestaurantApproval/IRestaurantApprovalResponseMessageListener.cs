using Rosered11.OrderService.Domain.DTO.Message;

namespace Rosered11.OrderService.Domain.Ports.Input.Message.Listener.RestaurantApproval
{
    public interface IRestaurantApprovalResponseMessageListener
    {
        void OrderApproved(RestaurantApprovalResponse restaurantApprovalResponse);
        void OrderRejected(RestaurantApprovalResponse restaurantApprovalResponse);
    }
}