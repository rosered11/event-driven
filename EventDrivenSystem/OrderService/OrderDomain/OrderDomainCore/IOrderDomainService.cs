using Rosered11.OrderService.Domain.Entities;
using Rosered11.OrderService.Domain.Event;

namespace Rosered11.OrderService.Domain
{
    public interface IOrderDomainService
    {
        OrderCreatedEvent ValidateAndInitiateOrder(Entities.Order? order, Restaurant? restaurant);
        OrderPaidEvent PayOrder(Entities.Order? order);
        void ApproveOrder(Entities.Order? order);
        OrderCancelledEvent CancelOrderPayment(Entities.Order? order, List<string> failureMessage);
        void CancelOrder (Entities.Order? order, List<string> failureMessage);
    }
}