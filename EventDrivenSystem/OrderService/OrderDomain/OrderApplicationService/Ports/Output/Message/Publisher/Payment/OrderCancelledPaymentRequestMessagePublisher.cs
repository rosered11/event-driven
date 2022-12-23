using Rosered11.OrderService.Common.Event.Publisher;
using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.Event;

namespace Rosered11.OrderService.Domain.Ports.Output.Message.Publisher.Payment
{
    public interface OrderCancelledPaymentRequestMessagePublisher : DomainEventPublisher<OrderCancelledEvent, Order>
    {
        
    }
}