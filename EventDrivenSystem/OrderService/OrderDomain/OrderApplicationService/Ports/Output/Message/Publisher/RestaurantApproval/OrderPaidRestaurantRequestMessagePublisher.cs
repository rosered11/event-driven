using Rosered11.OrderService.Common.Event.Publisher;
using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.Event;

namespace Rosered11.OrderService.Domain.Ports.Output.Message.Publisher.RestaurantApproval
{
    public interface OrderPaidRestaurantRequestMessagePublisher : DomainEventPublisher<OrderPaidEvent, Order>
    {
        
    }
}